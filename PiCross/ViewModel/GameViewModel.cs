﻿using Cells;
using CommonServiceLocator;
using DataStructures;
using PiCross;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utility;

namespace ViewModel
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public GameViewModel(MainViewModel mainViewModel, Puzzle Puzzle)
        {
            this.Vm = mainViewModel;
            this.Facade = new PiCrossFacade();
            this.PlayablePuzzle = Facade.CreatePlayablePuzzle(Puzzle);
            this.Start(mainViewModel, PlayablePuzzle);
            this.MenuCommand = new Command(() => this.Vm.StartView());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public PiCrossFacade Facade { get; }
        public Puzzle Puzzle;
        public IPlayablePuzzle PlayablePuzzle { get; private set; }
        public IGrid<PuzzleSquareViewModel> Grid { get; private set; }
        public MainViewModel Vm { get; private set; }
        public ICommand MenuCommand { get; }
        public Chronometer chronometer { get; private set; }

        public void Start(MainViewModel viewModel, IPlayablePuzzle puzzle)
        {
            this.Vm = viewModel;
            this.PlayablePuzzle = puzzle;
            this.Grid = this.PlayablePuzzle.Grid.Map(puzzlesquare => new PuzzleSquareViewModel(puzzlesquare)).Copy();
            this.chronometer = new Chronometer();
            this.chronometer.Start();
        }

        public Cell<bool> IsSolved
        {
            get
            {
                return PlayablePuzzle.IsSolved;
            }
        }
        public Cell<TimeSpan> time
        {
            get
            {
                return chronometer.TotalTime;
            }
        }
    }
}
