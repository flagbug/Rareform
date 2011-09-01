using System;
using FlagLib.Console.Controls;
using FlagLib.Measure;

namespace FlagLib.ConsoleTests
{
    internal class TestScreen : Screen
    {
        private enum MainMenuAction
        {
            Item1,
            Item2,
            Item3
        }

        private Menu<Action> mainMenu;

        private Panel mainMenuPanel;

        private Label mainManuTextLabel;

        public TestScreen()
        {
            this.mainMenuPanel = new Panel();
            this.mainMenuPanel.RelativePosition = new Position(2, 2);
            this.Controls.Add(this.mainMenuPanel);

            this.mainManuTextLabel = new Label();
            this.mainManuTextLabel.Size = new Size(25, 1);
            this.mainManuTextLabel.Text = "This is the main menu.";
            this.mainMenuPanel.Controls.Add(mainManuTextLabel);

            this.mainMenu = new Menu<Action>();
            this.mainMenu.RelativePosition = new Position(2, 2);
            this.mainMenu.Items.Add(new MenuItem<Action>("Change Text", this.ChangeTextMethod));
            this.mainMenu.Items.Add(new MenuItem<Action>("Hide Text", this.HideText));
            this.mainMenu.Items.Add(new MenuItem<Action>("Show Text", this.ShowText));

            this.mainMenu.UpKeys.Add(ConsoleKey.W);
            this.mainMenu.DownKeys.Add(ConsoleKey.S);

            this.mainMenu.ItemChosen += new EventHandler<MenuEventArgs<Action>>(mainMenu_ItemChosen);

            this.mainMenuPanel.Controls.Add(this.mainMenu);
        }

        private void mainMenu_ItemChosen(object sender, MenuEventArgs<Action> e)
        {
            e.Item.Value.Invoke();
            this.mainMenu.Focus();
        }

        public void Activate()
        {
            this.Update();
            this.mainMenu.Focus();
        }

        public void ChangeTextMethod()
        {
            this.mainManuTextLabel.Text = "Changed Text";
            this.Update();
        }

        public void HideText()
        {
            this.mainManuTextLabel.IsVisible = false;
            this.Update();
        }

        public void ShowText()
        {
            this.mainManuTextLabel.IsVisible = true;
            this.Update();
        }
    }
}