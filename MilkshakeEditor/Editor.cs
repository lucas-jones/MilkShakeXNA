using System;
using System.Windows.Forms;
using MilkShakeFramework;
using MilkShakeFramework.Components.Lighting.Lights;
using MilkShakeFramework.Components.Physics;
using MilkShakeFramework.Components.PostProccessing.Presets;
using MilkShakeFramework.Core;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Core.Game.Components.Audio;
using MilkShakeFramework.Core.Game.Components.Misc;
using MilkShakeFramework.Core.Game.Components.Water;
using MilkShakeFramework.Core.Scenes;
using Ra.Screens;
using Ra.Screens.Editor;
using Ra.Screens.Game.Global.GameObjects.Layers;
using Ra.Screens.Game.Global.Level;
using TreeNode = System.Windows.Forms.TreeNode;

namespace MilkshakeEditor
{
    public partial class Editor : Form
    {
        private EditorScene _currentScene;

        public Editor()
        {
            InitializeComponent();
        }

        public PictureBox PreviewPannel
        {
            get { return previewPannel; }
        }

        public GameEntity RaScene
        {
            get { return _currentScene.GetNodeByName<GameEntity>("RaScene"); }
        }

        public GameEntity SelectedEntity
        {
            get { return _currentScene.EditorTools.SelectedEntity ?? RaScene; }
        }

        public IntPtr getDrawSurface()
        {
            return previewPannel.Handle;
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            SceneManager.OnSceneChange += (scene) =>
                {
                    if (scene is EditorScene)
                    {
                        _currentScene = scene as EditorScene;
                        _currentScene.Listener.EntityAdded += OnEntityChanged;
                        _currentScene.Listener.EntityRemoved += OnEntityChanged;

                        _currentScene.EditorTools.OnEntitySelected += OnEntitySelected;

                        RefreshNodeView();
                    }
                };

            raGameObjectsToolStripMenuItem.DropDown.Items.Add("Player Layer", null,
                                                              (ea, d) =>
                                                              RaScene.AddNode(new PlayerLayer {Name = "PlayerLayer"}));
            raGameObjectsToolStripMenuItem.DropDown.Items.Add("Distortion Layer", null,
                                                              (ea, d) => RaScene.AddNode(new DistortionLayer()));
            raGameObjectsToolStripMenuItem.DropDown.Items.Add("Decal Layer", null,
                                                              (ea, d) => RaScene.AddNode(new DecalLayer()));

            raGameObjectsToolStripMenuItem.DropDown.Items.Add("Water Fall", null,
                                                              (ea, d) =>
                                                              SelectedEntity.AddNode(new WaterFall
                                                                  {
                                                                      Position = RaScene.Scene.Camera.Position
                                                                  }));

            propertyGrid1.PropertyValueChanged += propertyGrid1_PropertyValueChanged;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            RefreshNodeView();
        }

        private void OnEntitySelected(GameEntity entity)
        {
            MilkshakeEditorTreeNode node = FindNode(SceneTreeView.Nodes[0] as MilkshakeEditorTreeNode, entity);
            if (SceneTreeView.SelectedNode != node && node != null)
            {
                SceneTreeView.SelectedNode = node;
            }
        }

        private MilkshakeEditorTreeNode FindNode(MilkshakeEditorTreeNode tvSelection, INode entity)
        {
            foreach (MilkshakeEditorTreeNode node in tvSelection.Nodes)
            {
                if (node.Node == entity)
                {
                    return node;
                }
                else
                {
                    MilkshakeEditorTreeNode nodeChild = FindNode(node, entity);
                    if (nodeChild != null) return nodeChild;
                }
            }
            return null;
        }

        private void OnEntityChanged(Entity node)
        {
            RefreshNodeView();

            Console.WriteLine("Refresh");
        }

        private void RefreshNodeView()
        {
            if (_currentScene.Nodes.Count > 0)
            {
                SceneTreeView.Nodes.Clear();

                var scene = new TreeNode();
                CreateTree(_currentScene.GetNodeByName("RaScene"), scene);
                SceneTreeView.Nodes.Add(scene.Nodes[0]);
                SceneTreeView.Nodes[0].Expand();
            }
        }

        private void CreateTree(INode node, TreeNode treeNode)
        {
            if (node is ITreeNode)
            {
                var sytemNode = new MilkshakeEditorTreeNode(node);
                treeNode.Nodes.Add(sytemNode);
                (node as ITreeNode).Nodes.ForEach(n => CreateTree(n, sytemNode));
            }
            else
            {
                treeNode.Nodes.Add("[" + node.GetType().Name + "] " + node.Name);
            }
        }

        private void previewPannel_Click(object sender, EventArgs e)
        {
            previewPannel.Focus();
        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = (e.Node as MilkshakeEditorTreeNode).Node;
            _currentScene.EditorTools.SelectEntity((e.Node as MilkshakeEditorTreeNode).Node as GameEntity);
        }

        private void SelectToolBtn_Click(object sender, EventArgs e)
        {
            TransformToolBtn.Checked = false;
            PolygonToolBtn.Checked = false;

            _currentScene.EditorTools.CurrentTool = EditorTool.Select;
        }

        private void TransformBtn_Click(object sender, EventArgs e)
        {
            SelectToolBtn.Checked = false;
            PolygonToolBtn.Checked = false;

            _currentScene.EditorTools.CurrentTool = EditorTool.Transform;
        }

        private void PolygonToolBtn_Click(object sender, EventArgs e)
        {
            TransformToolBtn.Checked = false;
            SelectToolBtn.Checked = false;

            _currentScene.EditorTools.CurrentTool = EditorTool.Polygon;
        }

        private void RemoveEntityBtn_Click(object sender, EventArgs e)
        {
            if (_currentScene.EditorTools.SelectedEntity != null)
            {
                _currentScene.EditorTools.SelectedEntity.Parent.RemoveNode(_currentScene.EditorTools.SelectedEntity);
                _currentScene.EditorTools.SelectEntity(null);
            }
        }

        private void AddSpriteBtn_Click(object sender, EventArgs e)
        {
            SelectedEntity.AddNode(new Sprite("Scene//Temp//Longcat")
                {
                    Position = _currentScene.Camera.Position + Globals.ScreenCenter,
                    AutoCenter = true
                });
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedEntity.AddNode(new Group());
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RefreshNodeView();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _currentScene.DISPLAY_BOUNDING_BOX = toolStripButton2.Checked;
            _currentScene.ComponentManager.GetComponent<PhysicsComponent>().DrawDebug = toolStripButton2.Checked;
        }

        private void MoveForwardBtn_Click(object sender, EventArgs e)
        {
            int currentIndex = SelectedEntity.Parent.Nodes.IndexOf(SelectedEntity);

            if (currentIndex + 1 < SelectedEntity.Parent.Nodes.Count)
            {
                INode node = SelectedEntity.Parent.Nodes[currentIndex + 1];

                SelectedEntity.Parent.Nodes[currentIndex + 1] = SelectedEntity.Parent.Nodes[currentIndex];
                SelectedEntity.Parent.Nodes[currentIndex] = node;

                RefreshNodeView();

                _currentScene.EditorTools.SelectEntity(SelectedEntity.Parent.Nodes[currentIndex + 1] as GameEntity);
            }
        }

        private void MoveBackBtn_Click(object sender, EventArgs e)
        {
            int currentIndex = SelectedEntity.Parent.Nodes.IndexOf(SelectedEntity);

            if (currentIndex > 0)
            {
                INode node = SelectedEntity.Parent.Nodes[currentIndex - 1];

                SelectedEntity.Parent.Nodes[currentIndex - 1] = SelectedEntity.Parent.Nodes[currentIndex];
                SelectedEntity.Parent.Nodes[currentIndex] = node;

                RefreshNodeView();

                _currentScene.EditorTools.SelectEntity(SelectedEntity.Parent.Nodes[currentIndex - 1] as GameEntity);
            }
        }

        private void AddSoundBtn_Click(object sender, EventArgs e)
        {
            SelectedEntity.AddNode(new Sound("Scene//Levels//Sounds//seagull", true, true)
                {
                    Position = _currentScene.Camera.Position + Globals.ScreenCenter
                });
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedEntity.AddNode(new PointLight(512) {Position = _currentScene.Camera.Position + Globals.ScreenCenter});
        }

        private void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {
        }
    }

    public class MilkshakeEditorTreeNode : TreeNode
    {
        private readonly INode _node;

        public MilkshakeEditorTreeNode(INode node) : base("[" + node.GetType().Name + "] " + node.Name)
        {
            _node = node;

            if (_node is GameEntity) SetImage("brick");
            if (_node is Sprite) SetImage("picture");
            if (_node is Group) SetImage("folder");
            if (_node is Sound) SetImage("sound");

            if (_node is Water) SetImage("drink");
            if (_node is RamLevel) SetImage("world");
            if (_node is RamLevelRenderer) SetImage("paintbrush");
            if (_node is RaTerrain) SetImage("vector");
        }

        public INode Node
        {
            get { return _node; }
        }

        private void SetImage(string key)
        {
            ImageKey = key + ".png";
            SelectedImageKey = key + ".png";
        }
    }
}