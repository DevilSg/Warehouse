using System.IO;
using System.Windows;
using Microsoft.Win32;
using WpfApp1.Integration.Contracts;
using WpfApp1.Warehouse.Data.Contracts;


namespace WpfApp1;

public partial class MainWindow : Window
{
    private readonly IImportService _importService;
    private readonly IMarkingService _markingService;
    private readonly IExportService _exportService;
    private readonly IProductService _productService;
    private readonly IBoxService _boxService;
    private readonly IPalletService _palletService;


    public MainWindow(IImportService importService,
        IMarkingService markingService,
        IExportService exportService,
        IProductService productService,
        IBoxService boxService,
        IPalletService palletService
        )
    {
        _importService = importService;
        _markingService = markingService;
        _exportService = exportService;
        _productService = productService;
        _boxService = boxService;
        _palletService = palletService;

        InitializeComponent();

    }

    private void GetMap_OnClick(object sender, RoutedEventArgs e)
    {
        var gtin = _markingService.GetMission().Mission.Lot.Product.Gtin;
        var test = _exportService.GetMapByProductGtin(gtin);
        var fileName = $"{textgtin.Text}_result_file{DateTime.UtcNow.ToString("ddMMyy_hhmm")}";
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Json files (*.json)|*.json";
        saveFileDialog.FileName = fileName;
        if (saveFileDialog.ShowDialog() == true)
            File.WriteAllText(saveFileDialog.FileName, test);

    }
    private void ShowFileDialog_OnClick(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() != true) return;

        using var streamReader = new StreamReader(openFileDialog.OpenFile());
        var productCodes = new List<string>();
        while (streamReader.Peek() >= 0)
        {
            productCodes.Add(streamReader.ReadLine());
        }

        _importService.ImportProductFromFileByCode(productCodes);
    }

    private void GetMission_OnClick(object sender, RoutedEventArgs e)
    {
        var mission = _markingService.GetMission();
        textproduct.Text = mission.Mission.Lot.Product.Name;
        textgtin.Text = mission.Mission.Lot.Product.Gtin;
        textvolume.Text = mission.Mission.Lot.Package.Volume;
        textformatbox.Text = mission.Mission.Lot.Package.BoxFormat.ToString();
        textformatpallet.Text = mission.Mission.Lot.Package.PalletFormat.ToString();
    }

    private void TabItem_GotFocus(object sender, RoutedEventArgs e)
    {
        var products = _productService.GetAllByGtin(textgtin.Text);
        datagrid1.ItemsSource = products.ToList();
    }

    private void TabItem_GotFocus_1(object sender, RoutedEventArgs e)
    {
        var pallet = _palletService.GetAllByGtin(textgtin.Text);
        datagrid3.ItemsSource = pallet.ToList();
    }

    private void TabItem_GotFocus_2(object sender, RoutedEventArgs e)
    {
        var boxes = _boxService.GetAllByGtin(textgtin.Text);
        datagrid2.ItemsSource = boxes.ToList();
    }

}