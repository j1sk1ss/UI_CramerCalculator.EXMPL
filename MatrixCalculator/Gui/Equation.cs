using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MatrixCalculator.Windows;

namespace MatrixCalculator.Gui {
    public static class Equation {
        private static readonly List<char> VariableNames = new() {
            'X', 'Y', 'Z', 'K', 'B', 'P', 'M', 'C', 'F', 'G', 'L', 'U', 'Q', 'V', 'A'
        };
        public static Grid GetEquation(int size, QuestSetter questSetter) {
            var equationGrid = new Grid();
            
            const int mainIndent = 125;

            for (var i = 0; i < size; i++) {
                    var tempGrid = new Grid { // Создаём основу одного уравнения
                        Height = 40,
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    
                    for (var j = 0; j < size; j++) {
                        tempGrid.Children.Add(new ComboBox() {  // Закидываем туда необходимое кол-во всякой всячины
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Margin   = new Thickness(5 + mainIndent * j, 0, 0, 0),
                            Items    = { '+', {'-'} },
                            Width    = 50,
                            FontSize = 20
                        });
                        tempGrid.Children.Add(new TextBox() {
                            HorizontalAlignment = HorizontalAlignment.Left,                            
                            MaxLength = 3,
                            Height    = 40,
                            Width     = 40,
                            FontSize  = 20,
                            Margin    = new Thickness(60 + mainIndent * j, 0, 0, 0)
                        });
                        tempGrid.Children.Add(new Label() {
                            FontSize = 20,
                            Margin   = new Thickness(100 + j * mainIndent,0,0,0),
                            Content  = VariableNames[j]
                        });
                    }
                    
                    tempGrid.Children.Add(new Label() { // Добавляем в конец =, место под выбор +\- и для цифры 
                        FontSize = 20,
                        Margin   = new Thickness(0 + size * mainIndent,0,0,0),
                        Content  = '='
                    });
                    tempGrid.Children.Add(new ComboBox() {
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Width    = 50,
                        Margin   = new Thickness(35 + mainIndent * size, 0, 0, 0),
                        Items    = {'+', '-'},
                        FontSize = 20
                    });
                    tempGrid.Children.Add(new TextBox() {
                        HorizontalAlignment = HorizontalAlignment.Left,                        
                        MaxLength = 3,
                        Height    = 40,
                        Width     = 40,
                        FontSize  = 20,
                        Margin    = new Thickness(80 + mainIndent * size, 0, 0, 0)
                    });
                    tempGrid.Margin = new Thickness(0, 65 * i, 0, 0);
                    equationGrid.Children.Add(tempGrid);
            }

            const int buttonIndent = 130;
            
            var tempButton = new Button() { // Кнопки добавления и уменьшения кол-ва уравнений
                HorizontalAlignment = HorizontalAlignment.Left,                
                Width   = 25,
                Height  = 25,
                Margin  = new Thickness(buttonIndent + mainIndent * size, -12, 0,0),
                Content = '+'
            };
            tempButton.Click += questSetter.IncreaseMatrix;
            equationGrid.Children.Add(tempButton);
                
            tempButton = new Button() {
                HorizontalAlignment = HorizontalAlignment.Left,                
                Width   = 25,
                Height  = 25,
                Margin  = new Thickness(buttonIndent + mainIndent * size, 44, 0,0),
                Content = '-'
            };
            tempButton.Click += questSetter.DecreaseMatrix;
            equationGrid.Children.Add(tempButton);
            
            return equationGrid;
        }
    }
}