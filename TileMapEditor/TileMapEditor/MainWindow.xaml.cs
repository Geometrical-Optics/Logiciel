using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SFML.Graphics;

namespace TileMapEditor
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 


    public class TextureItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public class Cell
    {
        public string Bottom { get; set; } = "C:\\Users\\lenny\\Desktop\\wtf prolo.jpg";
        public string Top { get; set; } = "C:\\Users\\lenny\\Desktop\\wtf prolo.jpg";
        public string Wall { get; set; } = "C:\\Users\\lenny\\Desktop\\wtf prolo.jpg";

        public bool empty { get; set; } = false;
    }


    public partial class MainWindow : Window
    {


        private List<List<Cell>> _map;
        public List<List<Cell>> Map
        {
            get { return _map; }
            set { _map = value; }
        }

        private int _width = 15;
        private int _height = 15;



        private void fillMap()
        {
            Map = new List<List<Cell>>();
            for (int i = 0; i < _height; i++)
            {
                List<Cell> row = new List<Cell>();
                for (int j = 0; j < _width; j++)
                {
                    row.Add(new Cell { Bottom = "C:\\Users\\lenny\\Desktop\\coucoulescopains\\papu\\TileMapEditor\\TileMapEditor\\Ressources\\Textures\\lava.jpeg" });
                }
                Map.Add(row);
            }
        }




        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            textures = new ObservableCollection<TextureItem>() { new TextureItem { Name = "catfish", Path = "C:\\Users\\lenny\\Desktop\\coucoulescopains\\papu\\TileMapEditor\\TileMapEditor\\Ressources\\Textures\\cat.jpg" } };
            loadedTextures.ItemsSource = Textures;
            fillMap();
            CreateGrid();


        }

        private TextureItem selectedTexture = new TextureItem();

        private Cell cell = new Cell();


        private ObservableCollection<TextureItem> textures;
        public ObservableCollection<TextureItem> Textures
        {
            get { return textures; }
            set { textures = value; }
        }



        private void addTextureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Sélectionner une texture";
            openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = openFileDialog.SafeFileName;

                /*Image image = new Image();
                image.Source = new BitmapImage(new Uri(filePath));
                image.Height= image.Source.Height;
                image.Width= image.Source.Width;
                */
                TextureItem texture = new TextureItem { Name = fileName, Path = filePath };
                Textures.Add(texture);


            }
        }



        private void Save_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Sélectionner une texture";
            openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = openFileDialog.SafeFileName;

                /*Image image = new Image();
                image.Source = new BitmapImage(new Uri(filePath));
                image.Height= image.Source.Height;
                image.Width= image.Source.Width;
                */
                TextureItem texture = new TextureItem { Name = fileName, Path = filePath };
                Textures.Add(texture);


            }
        }

        private void myListView_SelectChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTexture = loadedTextures.SelectedItem as TextureItem;
        }






        private void CreateGrid()
        {

            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            // Create row and column definitions based on the size of the data
            for (int i = 0; i < _height; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < _width; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < _height; i++)
            {
                List<Cell> row = Map[i];
                for (int j = 0; j < _width; j++)
                {
                    string tex = Map[i][j].Bottom;

                    Border border = new Border()
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1),
                    };

                    System.Windows.Controls.Image image = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(tex, UriKind.RelativeOrAbsolute)), Width = 75, Height = 75 };
                    border.Child = image;
                    image.MouseDown += Image_MouseDown;
                    image.MouseRightButtonDown += TextBlock_ClickB;
                    image.Tag = new Tuple<int, int>(i, j);
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    grid.Children.Add(border);
                }

            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Get the clicked image
            System.Windows.Controls.Image clickedImage = (System.Windows.Controls.Image)sender;

            // Get the cell coordinates from the Tag property
            Tuple<int, int> cellCoordinates = (Tuple<int, int>)clickedImage.Tag;
            int row = cellCoordinates.Item1;
            int column = cellCoordinates.Item2;

            // Show the coordinates in a message box
            //MessageBox.Show($"Clicked cell: {row}, {column} {selectedTexture.Name}");
            cell = Map[row][column];
            LoadContentCell();
            LoadTextureCell();
        }


        private void TextBlock_ClickB(object sender, MouseButtonEventArgs e)
        {
            cell.Bottom = selectedTexture.Path;

            LoadContentCell();
            CreateGrid();
            LoadTextureCell();
        }

        private void TextBlock_ClickW(object sender, MouseButtonEventArgs e)
        {
            cell.Wall = selectedTexture.Path;

            LoadContentCell();
            LoadTextureCell();

        }

        private void TextBlock_ClickT(object sender, MouseButtonEventArgs e)
        {
            cell.Top = selectedTexture.Path;

            LoadContentCell();
            LoadTextureCell();

        }


        private void LoadTextureCell()
        {
            textureOfTheCell.Children.Clear();
            textureOfTheCell.RowDefinitions.Clear();
            textureOfTheCell.ColumnDefinitions.Clear();

            for (int i = 0; i < 2; i++)
            {
                textureOfTheCell.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 3; i++)
            {
                textureOfTheCell.ColumnDefinitions.Add(new ColumnDefinition());
            }

            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Bottom";
            textBlock.FontSize = 18;

            Grid.SetRow(textBlock, 1);
            Grid.SetColumn(textBlock, 0);
            textureOfTheCell.Children.Add(textBlock);

            textBlock = new TextBlock();
            textBlock.Text = "Wall";
            textBlock.FontSize = 18;
            Grid.SetRow(textBlock, 1);
            Grid.SetColumn(textBlock, 1);
            textureOfTheCell.Children.Add(textBlock);

            textBlock = new TextBlock();
            textBlock.Text = "Top";
            textBlock.FontSize = 18;
            Grid.SetRow(textBlock, 1);
            Grid.SetColumn(textBlock, 2);
            textureOfTheCell.Children.Add(textBlock);


            string tex = cell.Bottom;
            System.Windows.Controls.Image image = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(tex, UriKind.RelativeOrAbsolute)), Width = 150, Height = 150 };
            image.MouseDown += TextBlock_ClickB;
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 0);
            textureOfTheCell.Children.Add(image);

            tex = cell.Wall;
            image = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(tex, UriKind.RelativeOrAbsolute)), Width = 150, Height = 150 };
            image.MouseDown += TextBlock_ClickW;
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 1);
            textureOfTheCell.Children.Add(image);

            tex = cell.Top;
            image = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(tex, UriKind.RelativeOrAbsolute)), Width = 150, Height = 150 };
            image.MouseDown += TextBlock_ClickT;
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 2);
            textureOfTheCell.Children.Add(image);

        }

        private void LoadContentCell()
        {
            Patreon.Children.Clear();
            Patreon.RowDefinitions.Clear();
            Patreon.ColumnDefinitions.Clear();

            for (int i = 0; i < 4; i++)
            {
                Patreon.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 3; i++)
            {
                Patreon.ColumnDefinitions.Add(new ColumnDefinition());
            }


            string tex = cell.Top;
            System.Windows.Controls.Image image = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(tex, UriKind.RelativeOrAbsolute)), Width = 150, Height = 150 };
            Grid.SetRow(image, 0);
            Grid.SetColumn(image, 1);
            Patreon.Children.Add(image);

            tex = cell.Wall;
            image = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(tex, UriKind.RelativeOrAbsolute)), Width = 150, Height = 150 };
            Grid.SetRow(image, 1);
            Grid.SetColumn(image, 1);
            Patreon.Children.Add(image);


            tex = cell.Wall;
            image = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(tex, UriKind.RelativeOrAbsolute)), Width = 150, Height = 150 };
            Grid.SetRow(image, 2);
            Grid.SetColumn(image, 0);
            Patreon.Children.Add(image);


            tex = cell.Wall;
            image = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(tex, UriKind.RelativeOrAbsolute)), Width = 150, Height = 150 };
            Grid.SetRow(image, 2);
            Grid.SetColumn(image, 2);
            Patreon.Children.Add(image);


            tex = cell.Wall;
            image = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(tex, UriKind.RelativeOrAbsolute)), Width = 150, Height = 150 };
            Grid.SetRow(image, 3);
            Grid.SetColumn(image, 1);
            Patreon.Children.Add(image);


            tex = cell.Bottom;
            image = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(tex, UriKind.RelativeOrAbsolute)), Width = 150, Height = 150 };
            Grid.SetRow(image, 2);
            Grid.SetColumn(image, 1);
            Patreon.Children.Add(image);

        }


        private void About(object sender, RoutedEventArgs e)
        {
            string websiteUrl = "https://geometrical-optics.github.io/";
            System.Diagnostics.Process.Start(websiteUrl);
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }


    public class CarteSimplifie
    {
        public List<Box>[,] _Carte { get; private set; }
        public int _Height { get; private set; }
        public int _Width { get; private set; }
        public int _Depth { get; private set; }
        public int _ZSize { get; private set; }


        public List<Box> this[int x, int y]
        {
            get
            {
                return _Carte[x, y];
            }
            set
            {
                _Carte[x, y] = value;
            }
        }

        public Box this[int x, int y, int z]
        {
            get
            {
                return _Carte[x, y][z];
            }
            set
            {
                _Carte[x, y][z] = value;
            }
        }

        public CarteSimplifie((int Width, int Height, int Depth) Size)
        {
            _Height = Size.Height;
            _Width = Size.Width;
            _Depth = 1;
            _ZSize = Size.Depth;


            _Carte = new List<Box>[_Width, _Height];

            for (int i = 0; i < _Width; i++)
            {
                for (int j = 0; j < _Height; j++)
                {
                    _Carte[i, j] = new List<Box>() { new Empty((i, j), 1, 1, 2) };
                }
            }

            for (int i = 0; i < _Width; i++)
            {
                this[i, _Height - 1, 0] = new Full((i, _Height - 1), 1, 0, 0, 0, 0, 0);
                this[i, 0, 0] = new Full((i, 0), 1, 0, 0, 0, 0, 0);
            }

            for (int i = 0; i < _Height; i++)
            {
                this[_Width - 1, i, 0] = new Full((_Width - 1, i), 1, 0, 0, 0, 0, 0);
                this[0, i, 0] = new Full((0, i), 1, 0, 0, 0, 0, 0);
            }
        }


        public void Save(string path)
        {
            using (BinaryWriter sw = new BinaryWriter(File.Open(path, FileMode.Create), Encoding.UTF8, false))
            {
                sw.Write(_Width);
                sw.Write(_Height);
                sw.Write(_Depth);

                foreach (var chunk in _Carte)
                {
                    for (int i = 0; i < (int)(_Width); i++)
                    {
                        for (int j = 0; j < (int)(_Height); j++)
                        {
                            sw.Write(this[i, j].Count);
                            foreach (var VARIABLE in this[i, j])
                            {
                                VARIABLE.Save(path, sw);
                            }
                        }
                    }
                }
            }
        }

        public void SuperSave(string path, string name, SFML.Graphics.Image[] materials)
        {
            string way = path + name + '/';

            if (!Directory.Exists(way))
            {
                Directory.CreateDirectory(way);
            }

            Save(way + '/' + name + ".hep");

            for (int i = 0; i < materials.Length; i++)
            {
                string tmp = "img_";
                int w = i;
                while (w >= 10)
                {
                    tmp += "9";
                    w /= 10;
                }
                tmp += w;


                materials[i].SaveToFile(way + tmp + ".png");
            }
        }

        public void Load(string path)
        {

            if (File.Exists(path))
            {
                using (var stream = File.Open(path, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {

                        _Width = reader.ReadInt32();
                        _Height = reader.ReadInt32();
                        _Depth = reader.ReadInt32();

                        _Carte = new List<Box>[_Width, _Height];
                        for (int i = 0; i < (int)(_Width); i++)
                        {
                            for (int j = 0; j < (int)(_Height); j++)
                            {
                                _Carte[i, j] = new List<Box>();
                                int tmpdepth = reader.ReadInt32();

                                for (int k = 0; k < tmpdepth; k++)
                                {
                                    int type = reader.ReadInt32();

                                    if (type == 1)
                                    {
                                        _Carte[i, j].Add(Full.Read(path, reader));
                                    }
                                    else
                                    {
                                        _Carte[i, j].Add(Empty.Read(path, reader));
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public SFML.Graphics.Image[] SuperLoad(string path, string name)
        {
            Load(path + name + '/' + name + ".hep");
            string[] filePaths = Directory.GetFiles(path + name + '/', "*.png");
            filePaths = filePaths.OrderBy(x => x).ToArray();

            SFML.Graphics.Image[] result = new SFML.Graphics.Image[filePaths.Length];
            for (int i = 0; i < filePaths.Length; i++)
            {
                result[i] = new Texture(filePaths[i]).CopyToImage();
            }

            return result;
        }

        

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < _Width; i++)
            {
                for (int j = 0; j < _Height; j++)
                {
                    if (this[i, j, 0] is Empty)
                        result += '-';
                    else
                        result += '#';
                }

                result += '\n';
            }

            return result;
        }
    }


    public interface Box
    {
        (int X, int Y) _Coordinates { get; }
        (float X, float Y) _TexturePos { get; set; }
        (double X, double Y)[] _Vertex { get; }
        float _Height { get; }
        int _TextureId { get; set; }
        int _FloorId { get; set; }
        int _CeilingId { get; }
        int _TopDownId { get; }
        float _Size { get; }
        bool _IsTransparent { get; }

        bool _ContainsEntity { get; set; }
        float _posZ { get; }
        int _distance { get; set; }

        uint GetTextureX((double X, double Y) Coordinates, SFML.Graphics.Image Texture);

        void Save(string path, BinaryWriter sw);

        bool Same(Box tmp);
        //static void Read(string path, BinaryReader sr);
    }



    public class Full : Box
    {
        public (int X, int Y) _Coordinates { get; private set; }
        public (float X, float Y) _TexturePos { get; set; }
        public (double X, double Y)[] _Vertex { get; private set; }
        public float _Height { get; private set; }
        public int _TextureId { get; set; }
        public float _Size { get; private set; }
        public int _FloorId { get; set; }
        public int _CeilingId { get; private set; }

        public int _TopDownId { get; private set; }

        public bool _IsTransparent { get; private set; }

        public bool _ContainsEntity { get; set; }
        public float _posZ { get; private set; }
        public int _distance { get; set; }


        public Full((int X, int Y) Coordinates, float Height, int TextureId, int floor,
            int ceil, int topid, float pos_Z)
        {
            _Coordinates = Coordinates;
            _Height = Height;
            _TextureId = TextureId;
            _Vertex = new (double X, double Y)[1] { Coordinates };
            _Size = 1;
            _FloorId = floor;
            _CeilingId = ceil;
            _TopDownId = topid;
            _ContainsEntity = false;
            _TexturePos = (0, 0);
            _distance = 0;

            if (Height == 1 && pos_Z == 0)
                _IsTransparent = false;
            else
                _IsTransparent = true;

            _posZ = pos_Z;
        }


        public void Save(string path, BinaryWriter sw)
        {
            sw.Write(1);

            sw.Write(_distance);
            sw.Write(_Coordinates.X);
            sw.Write(_Coordinates.Y);
            sw.Write((double)_posZ);

            sw.Write((double)_TexturePos.X);
            sw.Write((double)_TexturePos.Y);

            sw.Write(_TextureId);
            sw.Write(_FloorId);
            sw.Write(_CeilingId);
            sw.Write(_TopDownId);
            sw.Write((double)_Height);
        }

        public static Box Read(string path, BinaryReader sr)
        {
            int x;
            int y;

            double x1;
            double y1;

            double z;
            int text;
            int floor;
            int ceil;
            int top;
            double height;

            var tempo = sr.ReadInt32();

            x = sr.ReadInt32();
            y = sr.ReadInt32();
            z = sr.ReadDouble();

            x1 = sr.ReadDouble();
            y1 = sr.ReadDouble();

            text = sr.ReadInt32();
            floor = sr.ReadInt32();
            ceil = sr.ReadInt32();
            top = sr.ReadInt32();
            height = sr.ReadDouble();

            var tmp = new Full((x, y), (float)height, text, floor, ceil, top, (float)z);

            tmp._TexturePos = ((float)x1, (float)y1);
            tmp._distance = tempo;

            return tmp;
        }

        public uint GetTextureX((double X, double Y) Coordinates, SFML.Graphics.Image Texture)
        {
            uint xx = (uint)(((Coordinates.X + _TexturePos.X) % 1) * (Texture.Size.X - 1));

            if (Coordinates.X % 1 < 0.02 || Coordinates.X % 1 > 0.98)
            {
                xx = (uint)(((Coordinates.Y + _TexturePos.X) % 1) * (Texture.Size.X - 1));
            }

            return xx;
        }

        public bool Same(Box tmp)
        {
            if (tmp._Coordinates == _Coordinates
                && tmp._Height == _Height
                && tmp._posZ == _posZ
                && tmp._Vertex == _Vertex)
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is Box)
                return Same((Box)(obj));
            return false;
        }
    }


    public class Empty : Box
    {
        public (int X, int Y) _Coordinates { get; private set; }
        public (float X, float Y) _TexturePos { get; set; }

        public (double X, double Y)[] _Vertex { get; private set; }
        public float _Height { get; private set; }
        public int _TextureId { get; set; }
        public float _Size { get; private set; }
        public int _FloorId { get; set; }
        public int _CeilingId { get; private set; }
        public int _TopDownId { get; private set; }
        public bool _IsTransparent { get; private set; }

        public bool _ContainsEntity { get; set; }
        public float _posZ { get; private set; }
        public int _distance { get; set; }


        public Empty((int X, int Y) Coordinates, float Height, int floor, int ceil)
        {
            _Coordinates = Coordinates;
            _Height = 0;
            _TextureId = 0;
            _Vertex = new (double X, double Y)[1] { Coordinates };
            _Size = 0;
            _FloorId = floor;
            _CeilingId = ceil;
            _IsTransparent = true;
            _posZ = 0;
            _TopDownId = 0;
            _ContainsEntity = false;
            _TexturePos = (0, 0);
            _distance = 0;
        }

        public void Save(string path, BinaryWriter sw)
        {
            sw.Write(0);
            sw.Write(_distance);
            sw.Write(_Coordinates.X);
            sw.Write(_Coordinates.Y);
            sw.Write(_FloorId);
            sw.Write(_CeilingId);
        }

        public static Box Read(string path, BinaryReader sr)
        {
            int x;
            int y;
            int floor;
            int ceil;

            var tempo = sr.ReadInt32();
            x = sr.ReadInt32();
            y = sr.ReadInt32();
            floor = sr.ReadInt32();
            ceil = sr.ReadInt32();

            var tmp = new Empty((x, y), 0, floor, ceil);
            tmp._distance = tempo;

            return tmp;
        }

        public uint GetTextureX((double X, double Y) Coordinates, SFML.Graphics.Image Texture)
        {
            return 0;
        }

        public bool Same(Box tmp)
        {
            if (tmp._Coordinates == _Coordinates
                && tmp._Height == _Height
                && tmp._posZ == _posZ
                && tmp._Vertex == _Vertex)
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is Box)
                return Same((Box)(obj));
            return false;
        }
    }

}