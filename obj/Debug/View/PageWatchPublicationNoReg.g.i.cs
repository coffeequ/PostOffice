﻿#pragma checksum "..\..\..\View\PageWatchPublicationNoReg.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "ADD89BEB6CFB78B894683132D6E123468A3A3F528A5EAC4EF32268A4B6ADA5A9"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using PostOffice.View;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PostOffice.View {
    
    
    /// <summary>
    /// PageWatchPublicationNoReg
    /// </summary>
    public partial class PageWatchPublicationNoReg : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 26 "..\..\..\View\PageWatchPublicationNoReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSearch;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\View\PageWatchPublicationNoReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbCatergoriaPublication;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\View\PageWatchPublicationNoReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbTypePublication;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\View\PageWatchPublicationNoReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbLogin;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\View\PageWatchPublicationNoReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView MyLv;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\View\PageWatchPublicationNoReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel spList;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\View\PageWatchPublicationNoReg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label InfoPages;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PostOffice;component/view/pagewatchpublicationnoreg.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            ((System.Windows.Controls.Grid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.GridLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbSearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            this.tbSearch.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextChangedSearch);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cbCatergoriaPublication = ((System.Windows.Controls.ComboBox)(target));
            
            #line 31 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            this.cbCatergoriaPublication.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbCategoriaChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbTypePublication = ((System.Windows.Controls.ComboBox)(target));
            
            #line 32 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            this.cbTypePublication.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbTypeChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 45 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnEntrance);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lbLogin = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.MyLv = ((System.Windows.Controls.ListView)(target));
            return;
            case 9:
            this.spList = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            
            #line 75 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnFirstPage);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 78 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnBackPage);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 81 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnNextPage);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 84 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnLastPage);
            
            #line default
            #line hidden
            return;
            case 14:
            this.InfoPages = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 8:
            
            #line 67 "..\..\..\View\PageWatchPublicationNoReg.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnWatch);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

