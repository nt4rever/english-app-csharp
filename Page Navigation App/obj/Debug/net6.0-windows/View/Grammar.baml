   M S B A M L   `   `   `  ????F  BEnglish App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Page_Navigation_App.ViewModelPage_Navigation_App.ViewModel  5      &     Page_Navigation_App.View.Grammar   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :vm+clr-namespace:Page_Navigation_App.ViewModel     5      _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5         
Page_Style#??[  5
      ???#fff  ? DataContext  5      -  'Page_Navigation_App.ViewModel.GrammarVM  5   
   .??5      ? .+?5   
   ??  ?? CornerRadius$ 	0,20,20,0??6   ??5      ?? 5       ?? ImageSource$ /Images/grammar_page.png??6    j? Stretch$
 None=?5      5      ?? 5   
   -   5        $
??Left=?5      $??Bottom=?6,   $??55??6G   $??55??6R   $
??Left=?6^   $??509,0,0,499q?6?   ??6?   Z? ??6?   Z?  ? 	Resources 5       ?? 
TargetType ?? Property ?? Value??%_   '??       ?? 5      " ??6   .?5      ??  ?@??CornerRadius$ 15??65   5      5      ??5      ?? 5      " ??6   .?5      ??  ? ??
 ???? 63   ?? 5        ? ?? 5!      ?? 5"       ?? 
TargetType" ??6.   .R?5#   "   ?? #??z??6)   ._?5$   &   ?? $??Center=?67   $??Center=?6T   5%   #   5&      5(      	 ?? Triggers	 5)      P? 5*      
 P? Property
 ?@J?IsMouseOver P? Value$
 True??69   . ?5+      ??  ? ??
 ?????6;   ?? 5,       ?@J?Opacity$	 0.1Q?68   5-      5/      51      ?? 52   
   -   5        $
??Left=?52      $	??Top=?6,   $??40??6D   $??40??6O   $
??Left=?6[   $??523,265,0,0q?6?   ??6}   Z? ??6?   Z?  53      ??%_   '??       ?? 54      " ??6   .?55      ??  ?@??CornerRadius$ 25??65   56      57      ??58      ?? 59      " ??6   .?5:      ??  ? ??
 ???? 63   ?? 5;       ? ?? 5<      ?? 5=      " ??6.   .R?5>   "   ?? #??z??6)   ._?5?   &   ?? $??Center=?67   $??Center=?6T   5@   #   5A      5C      	 5D      P? 5E      
 ?@J?IsMouseOver$
 True??69   . ?5F      ??  ? ??
 ?????6;   ?? 5G       ?@J?Opacity$	 0.1Q?68   5H      5J      5L      ? 5N   
    ? RowDefinitions 5O      ?? 5P      $	??17* ?6    6/   ?? 5Q      $	??13* ?6    6/   .+?5T      &   System.Windows.Controls.DataGrid  -   5        $??
tableSpell??5T      $??1??6)      
IsReadOnly$
 True??66   $??
55,0,56,38q?6H      Columns 5U      4  .System.Windows.Controls.DataGridTemplateColumn  5V      ,  &System.Windows.Controls.DataGridColumn   Header$ Message??6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 350 6>      CellTemplate 5W      ?? 5X      .R?5Y   "   ?? $
??Wrap=?5[      ??5Z      ?? *	Message+5[      5]      5_        5a      $ Replacement??6-   $	 150 6B    5b      ?? 5c      .R?5d   "   ?? $
??Wrap=?5f      ??5e      ?? *Replacement+5f      5h      5j      5l      ?? 5m      -   5        $??txtSpell??5n      $??0??5o      
??????  ?? TextWrapping$
 Wrap=?5q       ?? AcceptsReturn$
 True??5r      $??0q?5s      ???#fff$??16(?5u      $??/Fonts/#Rubik Regular)?5v       ?? 
CaretBrush ?#fff" ?? VerticalScrollBarVisibility$
 Auto=?5x      .  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Hidden=?5y      $??72,141,88,0q?6N   $	??159??6c   $	??Top=?6p   .??5z      5{      5|      5}      5~      