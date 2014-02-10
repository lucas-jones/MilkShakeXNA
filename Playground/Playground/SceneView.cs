using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core;
using System.Reflection;
using MilkShakeFramework.Core.Game;

namespace Playground
{
    public partial class SceneView : Form
    {
        private Scene _scene;

        public SceneView(Scene scene)
        {
            _scene = scene;

            _scene.Listener.EntityAdded += (node) => Update();
            _scene.Listener.EntityRemoved += (node) => Update();

            InitializeComponent();
        }

        private void SceneView_Load(object sender, EventArgs e)
        {            
            Update();
        }

        private void Update()
        {
            treeView1.Nodes.Clear();
            System.Windows.Forms.TreeNode rootNode = new System.Windows.Forms.TreeNode("Root");
            FetchNode(rootNode, _scene);
            treeView1.Nodes.Add(rootNode);

            treeView1.ExpandAll();
        }

        private void FetchNode(System.Windows.Forms.TreeNode treeNode, INode gameNode)
        {

            string name = (gameNode.Name == "Undefined") ? "" : " : " + gameNode.Name;

            if ((gameNode is GameEntity))
            {
                name += " " + (gameNode as GameEntity).Position;
            }


            if (gameNode is Sprite)
            {
                name += (gameNode as Sprite).Origin;
            }

            System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode(gameNode.GetType().Name  + name);


            if ((gameNode is Entity && (gameNode as Entity).IsLoaded))
            {

                PropertyInfo[] properties = gameNode.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                System.Windows.Forms.TreeNode propertiesNode = new System.Windows.Forms.TreeNode("Properties");

                foreach (PropertyInfo p in properties)
                {
                    object value = null;
                    try
                    {
                        value = p.GetValue(gameNode, null);
                    }
                    catch (Exception e)
                    {
                    }

                    if (value == null) value = "Error";

                    propertiesNode.Nodes.Add(p.Name + ": " + value);
                }

                //node.Nodes.Add(propertiesNode);

            }

           
            
            if (gameNode is MilkShakeFramework.Core.TreeNode)
            {
                MilkShakeFramework.Core.TreeNode gameTreeNode = gameNode as MilkShakeFramework.Core.TreeNode;
                foreach (INode childNode in gameTreeNode.Nodes)
                {
                    FetchNode(node, childNode);
                }
               
            }
            
            treeNode.Nodes.Add(node);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Update();
        }

    }
}
