   M S B A M L   `   `   `  ????K  GInvertoryTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:InvertoryTrackerInvertoryTracker  5   	   )    #InvertoryTracker.ShoppingListWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:InvertoryTracker     5   	     ? Title$  Shopping List??5   	   $	??450??6   $	??800??6,   
???????.??5
      ?  ? RowDefinitions 5   
   ?? 5      $
??auto ?6   ?? 5      $??1* ?6   ?? 5      $??20 ?6    ? ColumnDefinitions 5   
   ?? 5      $??35 ?6   ?? 5      $??1* ?6   ?? 5      $??35 ?6   .+?5   
   ?? $??Shopping List??6   $??0??6)   $??1??66   $??28(?6F   $??0,0,0,5q?6T   $
??Bold"?6e   $??Center=?6w   $??Center=?6?   $??Lucida Sans)?6?   ? 5   
   $??1??6   $??1??6    5      ?? 5      $
??auto ?6    ?? 5      $??1* ?6    ?? 5      $??1 ?6    ?? 5      $
??auto ?6    .+?5      ?? 
???   ?$??2??6(   &   System.Windows.Controls.DataGrid  5       -   5          Name$ 
dgShopList??5       $??0??6+      AutoGenerateColumns$ False??68      
IsReadOnly$
 True??6T      Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Item Name??6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   ?? *
ItemName+  5#      $ Available Quantity??6)    6E   ?? *AvailableQuantity+  5$      $ Minimum Quantity??6)    6C   ?? *MinimumQuantity+  5%      $ Location??6)    6;   ?? *
Location+  5&      $ Supplier??6)    6;   ?? *
Supplier+  5'      $ Category??6)    6;   ?? *
Category+5)      ?? 5,      -   5        	 ??Name$	 btnSaveShopList??5,      $??Save Shopping List??6.   $??Center=?6K   $??3??6h   $	??250??6u   $??40??6?   $??	0,20,0,20q?6?   $??20(?6?   $
??Bold"?6?   6?   5/      50      51      