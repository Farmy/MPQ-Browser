using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Mooege.Common.MPQ;
using System.Drawing;
using System.IO;
using System.Collections;
using Mooege.Core.GS.Common.Types.SNO;

namespace MPQBrowser
{
    class PropertyBrowser : TreeView
    {
        static readonly HashSet<Type> BuiltInTypes = new HashSet<Type> { typeof(object), typeof(string), typeof(int), typeof(byte), typeof(decimal), typeof(float), typeof(double), typeof(bool), typeof(sbyte), typeof(char), typeof(uint), typeof(double), typeof(long), typeof(ulong), typeof(short), typeof(ushort) };

        public delegate void OnSnoNodeClickHandler(int sno);
        public event OnSnoNodeClickHandler SnoNodeSelected;

        public PropertyBrowser(object element)
        {
            this.Font = new System.Drawing.Font("Courier New", 8);
            this.Dock = DockStyle.Fill;
            this.AfterSelect += new TreeViewEventHandler(PropertyBrowser_AfterSelect);
            this.MouseMove += new MouseEventHandler(PropertyBrowser_MouseMove);

            if (element != null)
            {
                TreeNode root = GetFieldsAndProperties(element);

                foreach (TreeNode node in root.Nodes)
                {
                    Nodes.Add(node);
                    if (node.Text.Contains("Header") == false)
                        node.Expand();
                }
            }
        }



        private TreeNode BasicTypeToNode(Type type, string name, string value)
        {
            if (type.IsEnum)
                return new TreeNode(name + " " + Enum.Parse(type, value));

            int intValue = 0;
            if (Int32.TryParse(value, out intValue))
                foreach (var asset in MPQStorage.Data.Assets.Values)
                    if (asset.ContainsKey(intValue))
                    {
                        TreeNode newNode = new TreeNode(name + " " + Path.GetFileName(asset[intValue].FileName) + " (" + intValue + ")");
                        newNode.Tag = intValue;
                        newNode.BackColor = Color.LightGreen;
                        return newNode;
                    }

            return new TreeNode(name + " " + value);
        }


        private TreeNode GetFieldsAndProperties(object element)
        {
            TreeNode node = new TreeNode();

            foreach (var member in element.GetType().GetMembers().Where(x => x is FieldInfo || x is PropertyInfo))
            {
                Type type = member is PropertyInfo ? (member as PropertyInfo).PropertyType : (member as FieldInfo).FieldType;
                bool indexed = member is PropertyInfo ? (member as PropertyInfo).GetIndexParameters().Length > 0 : false;
                Func<MemberInfo, object, object> getValue = (x, y) => x is PropertyInfo ? (x as PropertyInfo).GetValue(y, null) : (x as FieldInfo).GetValue(y);
                object value = getValue(member, element);


                if (BuiltInTypes.Contains(type) || type.IsEnum)
                {
                    node.Nodes.Add(BasicTypeToNode(type, member.Name, value == null ? "" : value.ToString()));
                }
                else
                {
                    TreeNode newNode = null;

                    if (indexed)
                    {
                        throw new NotImplementedException("Indexed fields and properties are not supported");
                    }
                    else
                    {
                        if (value != null)
                        {
                            var enumInterface = type.FindInterfaces((ty, c) => ty == typeof(IEnumerable), null);
                            if (enumInterface.Length > 0)
                            {
                                newNode = new TreeNode(member.Name);

                                foreach (object o in (IEnumerable)value)
                                {
                                    if (BuiltInTypes.Contains(o.GetType()) || o.GetType().IsEnum)
                                    {
                                        newNode.Nodes.Add(BasicTypeToNode(o.GetType(), member.Name, o == null ? "" : o.ToString()));
                                    }
                                    else
                                    {
                                        TreeNode childNode = GetFieldsAndProperties(o);
                                        childNode.Text = type.Name;
                                        newNode.Nodes.Add(childNode);
                                    }
                                }
                            }
                            else
                            {
                                newNode = GetFieldsAndProperties(value);
                                newNode.Text = member.Name;
                            }
                        }
                        else
                            newNode = new TreeNode(member.Name + " null");

                        node.Nodes.Add(newNode);
                    }
                }
            }
            return node;
        }


        void PropertyBrowser_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.GetNodeAt(e.Location) != null && this.GetNodeAt(e.Location).Tag != null)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Arrow;
        }

        void PropertyBrowser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
                if (SnoNodeSelected != null)
                    SnoNodeSelected((int)e.Node.Tag);
        }


    }




}
