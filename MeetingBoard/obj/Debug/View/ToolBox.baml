   M S B A M L   `   `   `  ����  MeetingBoard    MeetingBoard.View.ToolBox   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5          � Title$  Pen Settings��5   	   $	��150��6   $	��200��6+    � WindowStyle$
 none=�5   	    � WindowStartupLocation$ CenterOwner=�5   	    � Topmost .  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5
   	   
������ .��5      �� -   5        
������� �� CornerRadius$ 5,5,5,5��5   &    �� Padding$ 1q�6=   
�������$��1q�6`   |�.  ._�5   
   � -   5        $��MainGrid��5       +� IsFocusScope . 	 � RowDefinitions	 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      � $��0��6   .+�5      �� $�� Pen Settings��6   $��20��61   
�������$��Stretch=�6T   
����  �6�   5      �� 5      $��1��6   $��Color��6%   $��5, 0,5,0q�64   
����  �.��5      � -   5        $��	lineColor��5      $
��Left=�6-   .+�5      #  Xceed.Wpf.Toolkit.ColorPicker  -   5        $��cboColor��5   '   $��50��67   $��3��6B   
   DisplayColorAndName
 .  6k   5      5      �� 5      $��2��6   $��	Thickness��6$   $��5,0,5,0q�67   
����  �.��5      � -   5        $��lineThickness��5      .+�5      �� $��30��6[   $��Center=�6f   $��Center=�6�   
���   �$��	10,0,10,0q�6�   ��6    ��  �� ElementName$ BrushSizeSlider�� �� Path$ Value�6�   �� 5       -   5        $��18(�5       $��30��6+   $��15��67   
�������.��6w   -6z   �� 5!      -   5        $��BrushSizeSlider��5!      $��50��64    � Minimum$ 1Q�6?    � Maximum$ 8Q�6K    �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q�6r    � Value$ 2Q�6�   $��Center=�6�   6�   �� 5"      -	   5        $��18(�5"      $��30��6+   $��15��67   
�������.��6t   +6w   5#      5$      5%      5&      5'      