   M S B A M L   `   `   `  ����|  xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�rclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit)System.Windows.Controls.DataVisualization  5   	   �{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5   	   I EPictureFilters, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:PictureFiltersPictureFilters 5	   	      PictureFilters.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   �toolkitrclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	   �charts{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	   X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   (localclr-namespace:PictureFilters  5	   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � WindowState$ 	Maximized=�65    � 	Resources 5         DataPointStyle �� 
TargetTypeF   @System.Windows.Controls.DataVisualization.Charting.LineDataPoint �� Property �� Value  RedDataStyle �� BasedOn  GreenDataStyle  BlueDataStyle  DashedPolyLine  ChartMajorTickMarkStyle( "PictureFilters.ComparisonConverter  comparisonConverter��%|  &	        &	 �     7   &	 �     7   &	 2    7   &	 �    &	 �    &	     �� 5   
   "  6'   .�5      ��  � ��
 �����6,   �� 5       � ��$
 0.05q�60   �� 5       � �� 6)   Z� 5      �� 5   
   "  6%   8   .�5      ��  � ��
 �  ��6+   5      �� 5   
   "  6'   8   .�5      ��  � ��
 � � �6+   5      �� 5   
   "  6&   8   .�5      ��  � ��
 ��  �6+   5      �� 5   
   " �6'   .�5      ��  � ��$ 1��60   5      �� 5    
   " ��61   .�5!      ��  � y�$ 	Collapsed=�6+   5"        5#   
   6$   5$      .��5%      �  � ColumnDefinitions 5&   
   �� 5'      $	��300 �6   �� 5(      $	��200 �6   �� 5)      .+�5+   
   � $��0��6    � RowDefinitions 5,      �� 5-      $
��Auto �6    �� 5.      $
��Auto �6    �� 5/      .+�51      � $��0��6   .+�52      �� -   5        $	��100��52      $��50��6&   $��Center=�6J   $��Center=�6e   .��6�   Załaduj obraz6�   55      �� 56      $ ��Obszar zastosowania filtra��6   $��1��6;   $��15q�6H   .��57      �� .+�58      � $��15q�6    59      �� 5:      �� 5;      .+�5=      	� -   5        $��0��5=   '   	 	� 	GroupName$	 
filterArea��67   
 d� 	IsChecked
 6l   ��  �� Path$ 
FilterArea� �� 	Converter# [   PictureFilters.FilterArea   whole �� ConverterParameter# Z .��6�   Cały obraz6  �� 5>      -   5        $��1��5>   !   $��Zastosuj��6K   5?      � 5@      $��15q�6    5A      �� 5B      �� 5C      .+�5E      	� -   5        $��0��5E   &   $	 
filterArea��66   
 6k   �� $ 
FilterArea�# [    brush# Z .��6�   Pędzel kołowy6  �� 5F      $��1��6!    � Minimum$ 2Q�6T    � Maximum$ 50Q�6`    � Value 61   �� $ BrushRadius�6o   5G      � 5H      $��15q�6    5I      �� 5J      �� 5K      .+�5M      	� -   5        $��0��5M   &   $	 
filterArea��66   
 6k   �� $ 
FilterArea�# [    polygon# Z .��6�   	Wielokąt6  �� 5N      -   5        $��1��5N   !   .��6M   	Wyczyść6V   5O      5P      5Q      �� 5R      $ ��Wybór filtra macierzowego��6   $��2��6:   $��15q�6G   .��5S      �� -   5        $��filterStackpanel��5S      .+�5T      	� -   5        $��5q�5T   "   $	 filter��6-   
 6`   �� $ Filter.FilterType�# [   PictureFilters.FilterType   	identical# Z .��6�   Identyczność6  	� 5U      -	   5        $��5q�5U   "   $	 filter��6-   
 6`   �� $ Filter.FilterType�# [    blur# Z .��6�   
Rozmycie6�   	� 5V      -
   5        $��5q�5V   "   $	 filter��6-   
 6`   �� $ Filter.FilterType�# [    sharpen# Z .��6�   Wyostrzenie6  	� 5W      -   5        $��5q�5W   "   $	 filter��6-   
 6`   �� $ Filter.FilterType�# [    emboss# Z .��6�   Płaskorzeźba6  	� 5X      -   5        $��5q�5X   "   $	 filter��6-   
 6`   �� $ Filter.FilterType�# [    edges# Z .��6�   Wykrywanie krawędzi6  	� 5Y      -   5        $��5q�5Y   "   $	 filter��6-   
 6`   �� $ Filter.FilterType�# [    custom# Z .��6�   	Własny6�   � 5Z      -   5         �Name$ 
MatrixGrid��5Z      $��5q�6/    5[      �� 5\      �� 5]      �� 5^       5`      �� 5a      �� 5b      �� 5c      5e      5n      5o      5p      � 5q   
   $��1��6    5r      �� 5s      �� 5t      �� 5u      .+�5x      >   8System.Windows.Controls.DataVisualization.Charting.Chart  $��0��6      LegendStyle 5y      �� 5z      6   0System.Windows.Controls.DataVisualization.Legend"  6   .�5{      ��  � ��$ 0��62   5|         Series. 5~      C   =System.Windows.Controls.DataVisualization.Charting.LineSeries  ?   9System.Windows.Controls.DataVisualization.Charting.Series   Title$	 Red��6$   H	   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries 	  DependentValuePath$ Value��60    	  IndependentValuePath$	 Key��6K     	  DataPointStyle#  [ 6�   !   PolylineStyle#! [ 6�   " 	  ItemsSource" 6f   �� $ Histogram.Red�5�      5�        5�      $��1��6    5�      �� 5�      "  6   .�5�      ��  � ��$ 0��62   5�      . 5�        $ Green��6$   $ Value��62   $	 Key��6M   #  [ 6�   #! [ 6�   " 6h   �� $ Histogram.Green�6�   5�        5�      $��2��6    5�      �� 5�      "  6   .�5�      ��  � ��$ 0��62   5�      . 5�        $
 Blue��6$   $ Value��61   $	 Key��6L   #  [ 6�   #! [ 6�   " 6g   �� $ Histogram.Blue�6�   5�      5�      �� 5�   
   -   5        $��	MainImage��5�      $��2��6!   $
��None=�6�   $��Center=�6�   $��Center=�6�   $��10q�6  ��6�   �� $ Image�6  5�      5�      