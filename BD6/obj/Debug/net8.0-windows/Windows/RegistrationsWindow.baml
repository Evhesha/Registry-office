   M S B A M L   `   `   `  ����>  :BD6, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BD6.WindowsBD6.Windows  5   	   %    BD6.Windows.RegistrationsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:BD6.Windows     5   	     � Title$  RegistrationsWindow��5   	   $	��700��6%   $
��1000��62    � 
ResizeMode$ NoResize=�6?   
����İ�.��5	      � .+�5
   
   �� 
���   �$��1q�6%   $
��Left=�69   $	��305��6T   $��	692,4,0,0q�6a   $	��Top=�6t   $	��279��6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $	��195��6T   $��692,320,0,0q�6a   $	��Top=�6v   $	��279��6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $	��103��6T   $��692,551,0,0q�6a   $	��Top=�6v   $	��279��6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��RegistrationsDataGrid��5      $��14(�6V   $��	0,0,326,0q�6d      AutoGenerateColumns$ False��6w      Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Registrar��6)      CellTemplate 5      �� 5      .R�5      �� ��6(   �� *RegistrarDisplay+5         CellEditingTemplate 5      �� 5      .R�5      ��  �� DisplayMemberPath$ RegistrarDisplay��5       �� SelectedValuePath$ RegistrarId��5      ��5   '   �� 	 �� RelativeSource	 �� *FindAncestor+
 �� AncestorType"
 � �� Path$ RegistrarItems� �� SelectedValue 5      �� *RegistrarId+ �� UpdateSourceTrigger$ PropertyChanged=�5      5        5       $ Service��6)    5!      �� 5"      .R�5#      �� ��6(   �� *ServiceDisplay+5$       5&      �� 5'      .R�5(      �� $ ServiceDisplay��5)      $ 	ServiceId��5*      ��5(   '   �� 	 �� *FindAncestor+"
 �$ ServiceItems� 5+      �� *	ServiceId+$ PropertyChanged=�5,      5.        50      $ Client 1��6)    51      �� 52      .R�53      �� ��6(   �� *Client1Display+54       56      �� 57      .R�58      �� $ ClientDisplay��59      $ ClientId��5:      ��58   '   �� 	 �� *FindAncestor+"
 �$ ClientItems� 5;      �� *	ClientId1+$ PropertyChanged=�5<      5>        5@      $ Client 2��6)    5A      �� 5B      .R�5C      �� ��6(   �� *Client2Display+5D       5F      �� 5G      .R�5H      �� $ ClientDisplay��5I      $ ClientId��5J      ��5H   '   �� 	 �� *FindAncestor+"
 �$ ClientItems� 5K      �� *	ClientId2+$ PropertyChanged=�5L      5N      0  *System.Windows.Controls.DataGridTextColumn  5P      $
 Date��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *RegistrationDate+ �� StringFormat$ {0:dd.MM.yyyy}��  5Q      $ Price��6%    64   �� *ServicePrice+5S      �� 5U   
   -   5        $��
ReadButton��5U      $
��Read��6#   $
��Left=�62   $��36��6M   $��713,329,0,0q�6Y   $	��Top=�6n   $��91��6�   
�������6�   �� 5V   
   -   5        $��CancelButton��5V      $��Cancel��6%   $
��Left=�66   $��36��6Q   $��713,421,0,0q�6]   $	��Top=�6r   $��91��6�   
���Gc��6�   �� 5W   
   -   5        $��DeleteButton��5W      $��Delete��6%   $
��Left=�66   $��36��6Q   $��713,374,0,0q�6]   $	��Top=�6r   $��91��6�   
���Gc��6�   �� 5X   
   -   5        $��
SaveButton��5X      $
��Save��6#   $
��Left=�62   $��36��6M   $��713,469,0,0q�6Y   $	��Top=�6n   $��91��6�   
�������6�   �� 5Z   
   -   5        $��FirstButton��5Z      $��First��6$   $
��Left=�64   $��36��6O   $��822,329,0,0q�6[   $	��Top=�6p   $��91��6�   
�������6�   �� 5[   
   -   5        $��
NextButton��5[      $��Next ->��6#   $
��Left=�65   $��36��6P   $��822,374,0,0q�6\   $	��Top=�6q   $��91��6�   
�������6�   �� 5\   
   -   5        $��PreviousButton��5\      $��
Previous -��6'   $
��Left=�6<   $��36��6W   $��822,421,0,0q�6c   $	��Top=�6x   $��91��6�   
�������6�   �� 5]   
   -	   5        $��
LastButton��5]      $
��Last��6#   $
��Left=�62   $��36��6M   $��822,469,0,0q�6Y   $	��Top=�6n   $��91��6�   
�������6�   �� 5_   
   -
   5        $��RegistrarComboBox��5_      $
��Left=�6,   $��31��6G   $��
738,96,0,0q�6S   $	��Top=�6g   $	��158��6   �� 5`   
   -   5        $��ServiceComboBox��5`      $
��Left=�6*   $��31��6E   $��
738,56,0,0q�6Q   $	��Top=�6e   $	��158��6}   �� 5a   
   -   5        $��Client1ComboBox��5a      $
��Left=�6*   $��31��6E   $��738,138,0,0q�6Q   $	��Top=�6f   $	��158��6~   �� 5b   
   -   5        $��Client2ComboBox��5b      $
��Left=�6*   $��31��6E   $��738,180,0,0q�6Q   $	��Top=�6f   $	��158��6~   (  "System.Windows.Controls.DatePicker  5c   
   -   5        $��
DatePicker��5c      $
��Left=�6'   $��33��6B   $��
740,16,0,0q�6N   $	��Top=�6b   $	��158��6z   �� 5d   
   -   5        $��Price��5d   $    �� 
IsReadOnly$
 True��6   $
��Left=�61   $��31��6L   $��738,223,0,0q�6X    �� TextWrapping$
 Wrap=�6m   $��0��6�   $	��Top=�6�   $	��158��6�   �� 5f   
   $��
Registrars��6   $
��Left=�6%   $��
909,99,0,0q�6@   $	��Top=�6T   �� 5g   
   $��Services��6   $
��Left=�6#   $��
911,58,0,0q�6>   $	��Top=�6R   �� 5h   
   $��Client1��6   $
��Left=�6"   $��911,141,0,0q�6=   $	��Top=�6R   �� 5i   
   $��Client2��6   $
��Left=�6"   $��909,183,0,0q�6=   $	��Top=�6R   �� 5j   
   $
��Date��6   $
��Left=�6   $��
913,20,0,0q�6:   $	��Top=�6N   �� 5k   
   $��Price��6   $
��Left=�6    $��910,225,0,0q�6;   $	��Top=�6P   �� 5l   
   -   5        $��	AddButton��5l      $	��Add��6"   $
��Left=�60   $��36��6K   $��785,264,0,0q�6W   $	��Top=�6l   $��92��6�   
�������6�   �� 5m   
   -   5        $��ReportButton_Copy��5m      $��Create a report��6*   $
��Left=�6D   $��29��6_   $��717,573,0,0q�6k   $	��Top=�6�   $	��133��6�   6�   �� 5n   
   -   5        $��FilterButton��5n      $��Filter��6%   $
��Left=�66   $��29��6Q   $��717,607,0,0q�6]   $	��Top=�6r   $	��133��6�   6�   �� 5p   
   $!��/pictures/registrations.png��6   $��741,560,-65,35q�65   6O   �� 5q   
   $��Registrations��6   $
��Left=�6(   $��692,520,0,0q�6C   $	��Top=�6X   $��14(�6p   $��Black"�6~   �� 5s   
   -   5        $��ServiceWindowButton��5s      $��S��6,   $
��Left=�68   $��29��6S   $��
702,58,0,0q�6_   $	��Top=�6s   $��31��6�   6�   �� 5t   
   -   5        $��RegistrarsWindowButton��5t      $��R��6/   $
��Left=�6;   $��28��6V   $��
702,99,0,0q�6b   $	��Top=�6v   $��31��6�   6�   �� 5u   
   -   5        $��ClientsWindowButton��5u      $��C��6,   $
��Left=�68   $��31��6S   $��702,138,0,0q�6_   $	��Top=�6t   $��31��6�   6�   5v      5w      