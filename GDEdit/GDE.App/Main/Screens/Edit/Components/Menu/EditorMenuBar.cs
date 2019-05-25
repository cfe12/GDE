﻿using GDE.App.Main.Colors;
using GDE.App.Main.UI;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace GDE.App.Main.Screens.Edit.Components.Menu
{
    public class EditorMenuBar : GDEMenu
    {
        public EditorMenuBar()
            : base(Direction.Horizontal, true)
        {
            RelativeSizeAxes = Axes.X;

            MaskingContainer.CornerRadius = 0;
            ItemsContainer.Padding = new MarginPadding { Left = 100 };
            BackgroundColour = GDEColors.FromHex("111");
        }

        protected override osu.Framework.Graphics.UserInterface.Menu CreateSubMenu() => new SubMenu();

        protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item) => new DrawableEditorBarMenuItem(item);

        private class DrawableEditorBarMenuItem : DrawableGDEMenuItem
        {
            private BackgroundBox background;

            public DrawableEditorBarMenuItem(MenuItem item)
                : base(item)
            {
                Anchor = Anchor.CentreLeft;
                Origin = Anchor.CentreLeft;

                StateChanged += stateChanged;

                ForegroundColour = GDEColors.FromHex("8BB9FE");
                BackgroundColour = Color4.Transparent;
                ForegroundColourHover = Color4.White;
                BackgroundColourHover = GDEColors.FromHex("333");
            }

            public override void SetFlowDirection(Direction direction)
            {
                AutoSizeAxes = Axes.Both;
            }

            protected override void UpdateBackgroundColour()
            {
                if (State == MenuItemState.Selected)
                    Background.FadeColour(BackgroundColourHover);
                else
                    base.UpdateBackgroundColour();
            }

            protected override void UpdateForegroundColour()
            {
                if (State == MenuItemState.Selected)
                    Foreground.FadeColour(ForegroundColourHover);
                else
                    base.UpdateForegroundColour();
            }

            private void stateChanged(MenuItemState newState)
            {
                if (newState == MenuItemState.Selected)
                    background.Expand();
                else
                    background.Contract();
            }

            protected override Drawable CreateBackground() => background = new BackgroundBox();
            protected override DrawableGDEMenuItem.TextContainer CreateTextContainer() => new TextContainer();

            private new class TextContainer : DrawableGDEMenuItem.TextContainer
            {
                public TextContainer()
                {
                    NormalText.Font = NormalText.Font.With(size: 14);
                    BoldText.Font = BoldText.Font.With(size: 14);
                    NormalText.Margin = BoldText.Margin = new MarginPadding { Horizontal = 10, Vertical = MARGIN_VERTICAL };
                }
            }

            private class BackgroundBox : CompositeDrawable
            {
                private readonly Container innerBackground;

                public BackgroundBox()
                {
                    RelativeSizeAxes = Axes.Both;
                    Masking = true;
                    InternalChild = innerBackground = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Masking = true,
                        CornerRadius = 4,
                        Child = new Box { RelativeSizeAxes = Axes.Both }
                    };
                }

                /// <summary>Expands the background such that it doesn't show the bottom corners.</summary>
                public void Expand() => innerBackground.Height = 2;

                /// <summary>Contracts the background such that it shows the bottom corners.</summary>
                public void Contract() => innerBackground.Height = 1;
            }
        }

        private class SubMenu : GDEMenu
        {
            public SubMenu()
                : base(Direction.Vertical)
            {
                OriginPosition = new Vector2(5, 1);
                ItemsContainer.Padding = new MarginPadding { Top = 5, Bottom = 5 };

                BackgroundColour = GDEColors.FromHex("333");
            }

            protected override osu.Framework.Graphics.UserInterface.Menu CreateSubMenu() => new SubMenu();

            protected override DrawableMenuItem CreateDrawableMenuItem(MenuItem item) => new DrawableSubMenuItem(item);

            private class DrawableSubMenuItem : DrawableGDEMenuItem
            {
                public DrawableSubMenuItem(MenuItem item)
                    : base(item) { }

                protected override bool OnHover(HoverEvent e)
                {
                    if (Item is EditorMenuItemSpacer)
                        return true;

                    return base.OnHover(e);
                }

                protected override bool OnClick(ClickEvent e)
                {
                    if (Item is EditorMenuItemSpacer)
                        return true;

                    return base.OnClick(e);
                }
            }
        }
    }
}
