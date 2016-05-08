﻿#pragma checksum "..\..\ForumWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6E769CA00A970EA7DDEEBC71F297126C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GUI;
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


namespace GUI {
    
    
    /// <summary>
    /// ForumWindow
    /// </summary>
    public partial class ForumWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView subForumsListView;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image logo;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label passwordLbl;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label userNameLbl;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userNameTxt;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button registerBtn;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button loginBtn;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passwordTxt;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button forumsBtn;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addSubForumBtn;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logoutBtn;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sendPrivateMessageBtn;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\ForumWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock loggedInTxt;
        
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
            System.Uri resourceLocater = new System.Uri("/GUI;component/forumwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ForumWindow.xaml"
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
            this.subForumsListView = ((System.Windows.Controls.ListView)(target));
            
            #line 11 "..\..\ForumWindow.xaml"
            this.subForumsListView.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.subForumsListView_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.logo = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.passwordLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.userNameLbl = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.userNameTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.registerBtn = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\ForumWindow.xaml"
            this.registerBtn.Click += new System.Windows.RoutedEventHandler(this.registerBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.loginBtn = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\ForumWindow.xaml"
            this.loginBtn.Click += new System.Windows.RoutedEventHandler(this.loginBtn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.passwordTxt = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 9:
            this.forumsBtn = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\ForumWindow.xaml"
            this.forumsBtn.Click += new System.Windows.RoutedEventHandler(this.forumsBtn_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.addSubForumBtn = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\ForumWindow.xaml"
            this.addSubForumBtn.Click += new System.Windows.RoutedEventHandler(this.addSubForumBtn_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.logoutBtn = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\ForumWindow.xaml"
            this.logoutBtn.Click += new System.Windows.RoutedEventHandler(this.logoutBtn_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.sendPrivateMessageBtn = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\ForumWindow.xaml"
            this.sendPrivateMessageBtn.Click += new System.Windows.RoutedEventHandler(this.sendMsgBtn_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.loggedInTxt = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
