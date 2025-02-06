using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Models;



namespace battle_GUI.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        private object? _content;
        private bool _isAccessible;

        /// <summary>
        /// Координата X ячейки.
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Координата Y ячейки.
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Содержимое ячейки (юнит, препятствие или null).
        /// </summary>
        public object? Content
        {
            get => _content;
            set
            {
                if (_content != value)
                {
                    _content = value;
                    UpdateAccessibility();
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsOccupied));
                }
            }
        }

        /// <summary>
        /// Указывает, занята ли ячейка.
        /// </summary>
        public bool IsOccupied => Content != null;

        /// <summary>
        /// Является ли ячейка доступной для перемещения.
        /// </summary>
        public bool IsAccessible
        {
            get => _isAccessible;
            private set
            {
                if (_isAccessible != value)
                {
                    _isAccessible = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Конструктор для создания ViewModel ячейки.
        /// </summary>
        /// <param name="cell">Модель ячейки.</param>
        public CellViewModel(Cell cell)
        {
            X = cell.Position.X;
            Y = cell.Position.Y;
            Content = cell.Content;
            UpdateAccessibility();
        }

        /// <summary>
        /// Обновляет доступность ячейки для перемещения.
        /// </summary>
        private void UpdateAccessibility()
        {
            IsAccessible = Content switch
            {
                null => true, // Пустая ячейка доступна.
                Obstacle => false, // Препятствия недоступны.
                IUnit => false, // Ячейки с юнитами недоступны.
                _ => false // Любое другое содержимое недоступно.
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
}




