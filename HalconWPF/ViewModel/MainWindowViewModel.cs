﻿
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HalconWPF.Model;
using HalconWPF.UserControl;
using ICSharpCode.AvalonEdit;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace HalconWPF.ViewModel
{
    ///
    /// ----------------------------------------------------------------
    /// Copyright @Taosy.W 2021 All rights reserved
    /// Author      : Taosy.W
    /// Created Time: 2021/8/18 23:30:04
    /// Description :
    /// ------------------------------------------------------
    /// Version      Modified Time         Modified By    Modified Content
    /// V1.0.0.0     2021/8/18 23:30:04    Taosy.W                 
    ///
    public class MainWindowViewModel : ViewModelBase
    {
        // 控件
        private Grid MainContent;
        private TextEditor TextContainer;

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set => Set(ref selectedIndex, value);
        }

        private ObservableCollection<DataModel> dataList;
        public ObservableCollection<DataModel> DataList
        {
            get => dataList;
            set => Set(ref dataList, value);
        }

        /// <summary>
        /// 传个控件进来，没别的意思
        /// </summary>
        public RelayCommand<RoutedEventArgs> CmdLoaded => new Lazy<RelayCommand<RoutedEventArgs>>(() => new RelayCommand<RoutedEventArgs>(Loaded)).Value;
        private void Loaded(RoutedEventArgs e)
        {
            MainContent = (e.Source as MainWindow).MainContent;
            TextContainer = (e.Source as MainWindow).TextContainer;
        }

        /// <summary>
        /// 加载源代码
        /// </summary>
        public RelayCommand<bool> CmdShowSourceCode => new Lazy<RelayCommand<bool>>(() => new RelayCommand<bool>(ShowSourceCode)).Value;
        private void ShowSourceCode(bool isChecked)
        {
            if (!isChecked || SelectedIndex < 0)
            {
                return;
            }
            string name = DataList[SelectedIndex].Name;
            string filename = @"D:\MyPrograms\VisualStudio2019\WPFprograms\WPFSamples\HalconWPF\Resource\Halcon\" + name + ".txt";
            if (File.Exists(filename))
            {
                TextContainer.Load(filename);
            }
            else
            {
                TextContainer.Clear();
            }
        }

        /// <summary>
        /// 切换功能页面
        /// </summary>
        public RelayCommand CmdSelectionChanged => new Lazy<RelayCommand>(() => new RelayCommand(SelectionChanged)).Value;
        private void SelectionChanged()
        {
            if (SelectedIndex < 0)
            {
                return;
            };
            MainContent.Children.Clear();
            string name = DataList[SelectedIndex].Name;
            if (name == "AcquisitionImage")
            {
                _ = MainContent.Children.Add(new AcquisitionImage());
            }
            else if (name == "ImageReadSave")
            {
                _ = MainContent.Children.Add(new ImageReadSave());
            }
            else if (name == "ClipNumberAndAngle")
            {
                _ = MainContent.Children.Add(new ClipNumberAndAngle());
            }
            else if (name == "CircleFitting")
            {
                _ = MainContent.Children.Add(new CircleFitting());
            }
            else if (name == "PcbDefectDetection")
            {
                _ = MainContent.Children.Add(new PcbDefectDetection());
            }
            else if (name == "CalibrationWithPoints")
            {
                _ = MainContent.Children.Add(new CalibrationWithPoints());
            }
            else if (name == "BearingDefectDetection")
            {
                _ = MainContent.Children.Add(new BearingDefectDetection());
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainWindowViewModel()
        {
            SelectedIndex = -1;
            DataList = GetDataList();
        }

        private ObservableCollection<DataModel> GetDataList()
        {
            return new ObservableCollection<DataModel>
            {
                new DataModel{ Name = "AcquisitionImage", ImgPath="pack://application:,,,/Resource/Image/A.png"},
                new DataModel{ Name = "ImageReadSave", ImgPath="pack://application:,,,/Resource/Image/I.png"},
                new DataModel{ Name = "ClipNumberAndAngle", ImgPath="pack://application:,,,/Resource/Image/C.png"},
                new DataModel{ Name = "CircleFitting", ImgPath="pack://application:,,,/Resource/Image/C.png"},
                new DataModel{ Name = "PcbDefectDetection", ImgPath="pack://application:,,,/Resource/Image/P.png"},
                new DataModel{ Name = "CalibrationWithPoints", ImgPath="pack://application:,,,/Resource/Image/C.png"},
                new DataModel{ Name = "BearingDefectDetection", ImgPath="pack://application:,,,/Resource/Image/B.png"},
            };
        }
    }
}
