using System;
using System.Drawing;

namespace GLFW
{
    /// <summary>
    ///     Arguments supplied with mouse movement events.
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class MouseMoveEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MouseMoveEventArgs" /> class.
        /// </summary>
        /// <param name="x">
        ///     The cursor X-coordinate, relative to the left edge of the client area, or the amount of movement on
        ///     X-axis if this is scroll event.
        /// </param>
        /// <param name="y">
        ///     The cursor Y-coordinate, relative to the left edge of the client area, or the amount of movement on
        ///     Y-axis if this is scroll event.
        /// </param>
        public MouseMoveEventArgs(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the position of the mouse, relative to the screen.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public Point Position => new Point(Convert.ToInt32(X), Convert.ToInt32(Y));

        /// <summary>
        ///     Gets the cursor X-coordinate, relative to the left edge of the client area, or the amount of movement on
        ///     X-axis if this is scroll event.
        /// </summary>
        /// <value>
        ///     The location on the X-axis.
        /// </value>
        public double X { get; }

        /// <summary>
        ///     Gets the cursor Y-coordinate, relative to the left edge of the client area, or the amount of movement on
        ///     Y-axis if this is scroll event.
        /// </summary>
        /// <value>
        ///     The location on the Y-axis.
        /// </value>
        public double Y { get; }

        #endregion
    }
}