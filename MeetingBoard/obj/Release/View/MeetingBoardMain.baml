   M S B A M L   `   `   `  ���� E  AUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:UIControls;assembly=UIControls
UIControls  G CMeetingBoard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MeetingBoardMeetingBoard A%clr-namespace:MeetingBoard.PropertiesMeetingBoard.Properties (   "MeetingBoard.View.MeetingBoardMain   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  9search,clr-namespace:UIControls;assembly=UIControls  )controlsclr-namespace:MeetingBoard  6
properties%clr-namespace:MeetingBoard.Properties  :d2http://schemas.microsoft.com/expression/blend/2008  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     � Title$  MeetingBoard��$
��1024��$
��1280�� � WindowState$ 	Maximized=�.��� -    �Name$ MenuGrid��
������� � RowDefinitions �� $��30 ��� $��* �.+�]� 
�������$��0��.+�o� $	��Top=�.@�m� $��My Workspace��
����  �$��16(�.@�m� -   $��miNew��$	��New��$��14(�m� -   $��miOpen��$
��Open��$��14(�m� -   $��miClose��$��Close��$��14(�{�.  �� m� -   $��	miSaveAll��$��Save All��$��14(�{�.  � -   $ MainGrid��
�������$��1��$y�Hidden=� � ColumnDefinitions �� $��60 ��� $��* �.+�� $��0�� � Orientation$ Vertical=�$
��Left=�$��Center=�$��5q�
�������.+��� -    ��Name$ cmdDraw��$��5q� � ToolTip$
 Draw��
�������.���� $��..\Images\WSPencil.png�� J� Opacity$	 0.9Q�$
��None=�	 �� BitmapScalingMode$	 HighQuality=�d� -	   
 d�Name$
 
cmdToolbox��$��5q�$ Pen Settings��
�������.���� $��..\Images\Toolbox.png��$	 0.9Q�$
��None=�$	 HighQuality=��� -
   $ cmdErase��$��5q�$ Erase��
�������.���� $��..\Images\WSErase.png��$	 0.9Q�$
��None=�$	 HighQuality=��� -   $ cmdEraseStroke��$��5q�$ Erase a stroke��
�������.���� $!��..\Images\WSEraseStroke.png��$	 0.9Q�$
��None=�$	 HighQuality=��� -   $ cmdClearAll��$��5q�$ 	Erase all��
�������.���� $!��..\Images\WSEraseStroke.png��$	 0.9Q�$
��None=�$	 HighQuality=��� -   $ 	cmdSelect��$��5q�$ Select area��
�������.���� $��..\Images\WSSelect.png��$	 0.9Q�$
��None=�$	 HighQuality=��� -   $ 	cmdZoomIn��$��5q�$ Zoom In��
�������.���� $��..\Images\WSZoomIn.png��$	 0.9Q�$
��None=�$	 HighQuality=��� -   $ 
cmdZoomOut��$��5q�$ Zoom Out��
�������.���� $��..\Images\WSZoomOut.png��$	 0.9Q�$
��None=�$	 HighQuality=�� $��1��.+��� -    ��Name$ MainTabControl��$��18(�$��5q� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%V  '��       �� " ��.���  � �� �� " ��.R��� $��Border��$��2,2,2,1q�
�������$ 6,6,0,0��$	��2,0q�._��� $ ContentSite��$��Center=�$��Center=�$��Header��$
��10,2q� P�  �@��
IsSelected . . ��� $ Border�� � ��
 �����P�  �@��
IsSelected .  . ��� $ Border�� � ��
 �����.@��� -    ��Name$ TabGoals������  �� Orientation$ 
Horizontal=�.+��� $��Goals ��
����  �$
��Bold"��� -   $$��GoalsCurrentElapsedTimeDisplay��$��
(00:00:00)��
�������.���� $
��Auto=�$
��Auto=�$��Stretch=�$��Stretch=�.��� -   $ 	GoalsGrid�� � DataContext$  ��
������� �� $��15 ��� $��* ��� $��15 � �� $��15 ��� $��* ��� $��15 �.+��� -    ��Name$ GoalsBorder��$��1��$��1��$��Stretch=�$��Stretch=�
�������$��3q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ DarkGray���� -    ��Name$ GoalsCanvas��$��1��$��1��$��Stretch=�$��Stretch=�.H��� -    ��Name$ lblGoals��$��50q�$>��8What do we want to accomplish in today's design meeting?��$��24(�
�������$��Italic$�$��Times New Roman)��� -   $ 
TabEssence������ $ 
Horizontal=�.+��� $��Essence ��
����  �$
��Bold"��� -   $&�� EssenceCurrentElapsedTimeDisplay��$��
(00:00:00)��
�������.���� $
��Auto=�$
��Auto=�$��Stretch=�$��Stretch=�.��� -   $ EssenceGrid��$  ��
������� �� $��25 ��� $��* ��� $��25 � �� $��25 ��� $��* ��� $��25 �.+��� -   $ EssenceBorder��$��1��$��1��$��Stretch=�$��Stretch=�
�������$��3q�   $ DarkGray���� -   $ EssenceCanvas��$��1��$��1��$��Stretch=�$��Stretch=�.H��� -   $ 
lblEssence��$��50q�$a��[What is the most important/most challenging part of the design problem for us to get right?��$��24(�
�������$��Italic$�$��Times New Roman)��� -   $ TabConstraints������ $ 
Horizontal=�.+��� $��Constraints ��
����  �$
��Bold"��� -   $*��$ConstraintsCurrentElapsedTimeDisplay��$��
(00:00:00)��
�������.���� $
��Auto=�$
��Auto=�$��Stretch=�$��Stretch=�.��� -   $ ConstraintsGrid��$  ��
������� �� $��25 ��� $��* ��� $��25 � �� $��25 ��� $��* ��� $��25 �.+��� -    $ ConstraintsBorder��$��1��$��1��$��Stretch=�$��Stretch=�
�������$��3q�   $ DarkGray���� -!   $ ConstraintsCanvas��$��1��$��1��$��Stretch=�$��Stretch=�.H��� -"   $ lblConstraints��$��50q�$O��I What factors, technological or otherwise, limit the choices we can make?��$��24(�
�������$��Italic$�$��Times New Roman)��� -#   $ TabAssumptions������ $ 
Horizontal=�.+��� $��Assumptions ��
����  �$
��Bold"��� -$   $*��$AssumptionsCurrentElapsedTimeDisplay��$��
(00:00:00)��
�������.���� $
��Auto=�$
��Auto=�$��Stretch=�$��Stretch=�.��� -%   $ AssumptionsGrid��$  ��
������� �� $��25 ��� $��* ��� $��25 � �� $��25 ��� $��* ��� $��25 �.+��� -&   $ AssumptionsBorder��$��1��$��1��$��Stretch=�$��Stretch=�
�������$��3q�   $ DarkGray���� -'   $ AssumptionsCanvas��$��1��$��1��$��Stretch=�$��Stretch=�.H��� -(   $ lblAssumptions��$��50q�$`��Z What aspects of our design rest on a belief that, if false, would pose a serious problem?��$��24(�
�������$��Italic$�$��Times New Roman)��� -)   $ TabAlternatives������ $ 
Horizontal=�.+��� $��Alternatives ��
����  �$
��Bold"��� -*   $+��%AlternativesCurrentElapsedTimeDisplay��$��
(00:00:00)��
�������.���� $
��Auto=�$
��Auto=�$��Stretch=�$��Stretch=�.��� -+   $ AlternativesGrid��$  ��
������� �� $��25 ��� $��* ��� $��25 � �� $��25 ��� $��* ��� $��25 �.+��� -,   $ AlternativesBorder��$��1��$��1��$��Stretch=�$��Stretch=�
�������$��3q�   $ DarkGray���� --   $ AlternativesCanvas��$��1��$��1��$��Stretch=�$��Stretch=�.H��� -.   $ lblAlternatives��$��50q�$<��6What are different ways of solving the design problem?��$��24(�
�������$��Italic$�$��Times New Roman)��� -/   $ 	TabImpDec������ $ 
Horizontal=�.+��� $��
Decisions ��
����  �$
��Bold"��� -0   $%��ImpDecCurrentElapsedTimeDisplay��$��
(00:00:00)��
�������.���� $
��Auto=�$
��Auto=�$��Stretch=�$��Stretch=�.��� -1   $ 
ImpDesGrid��$  ��
������� �� $��25 ��� $��* ��� $��25 � �� $��25 ��� $��* ��� $��25 �.+��� -2   $ ImpDecBorder��$��1��$��1��$��Stretch=�$��Stretch=�
�������$��3q�   $ DarkGray���� -3   $ ImpDecCanvas��$��1��$��1��$��Stretch=�$��Stretch=�.H��� -4   $ 	lblImpDec��$��50q�$-��' What are the key choices that we made?��$��24(�
�������$��Italic$�$��Times New Roman)�