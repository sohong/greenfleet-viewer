   M S B A M L   `   `   `  ����  Viewer.Common"    Viewer.Common.View.VideoView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5	   
    5
      �� 5      $4 ./Viewer.Common;component/Resources/Shared.xaml?�6%   �� 5      $4 ./Viewer.Common;component/Resources/Slider.xaml?�6%   %    5      .��5      � ���#333333 � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � 
���   �.+�5      r� -   5        $��	mediaMain��5       r� LoadedBehavior$ Manual=�6,    r� ScrubbingEnabled .  r� UnloadedBehavior$
 Stop=�5      6S   5      � 5   
   $��1��6   
���   � 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� -   5        $��timelineSlider��5      $��	10,0,10,0q�6+      HorizontalSlider#��[  6>    �� IsMoveToPointEnabled . 6�   ]� 5      $��1��6   $��	10,0,10,0q�6%   ��. .+�5       �� -   5        $��btnPlay��5       $��2q�6(   $��2q�63   .��6U   Play6[   �� 5!      -   5        $��btnStop��5!      $��2q�6(   $��2q�63   .��6U   Stop6[   �� 5"      -   5        $��btnPause��5"      $��2q�6)   $��2q�64   .��6W   Pause6^   �� 5#      -   5        $��btnHome��5#      $��2q�6(   $��2q�63   .��6U   Home6[   �� 5$      -   5        $��btnPrevious��5$      $��2q�6,   $��2q�67   $	�� < ��6]   6p   �� 5%      -   5        $��btnNext��5%      $��2q�6(   $��2q�63   $	�� > ��6U   6h   �� 5&      -	   5        $��btnEnd��5&      $��2q�6'   $��2q�62   .��6S   End6X   �� 5'      -
   5        $��txtName��5'      
�������$��Right=�6>   $��Center=�6Z   [ No Track ]6�   5(      5)      5*      5+      