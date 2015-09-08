﻿// The MIT License (MIT)
// 
// Copyright (c) 2014-2015, Institute for Software & Systems Engineering
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace PressureTank
{
	using System;
	using System.Diagnostics;
	using System.Runtime.CompilerServices;
	using SafetySharp.Modeling;

	/// <summary>
	///   Represents a timer that signals a timeout.
	/// </summary>
	public class Timer : Component
	{
		/// <summary>
		///   The timeout signaled by the timer.
		/// </summary>
		private readonly int _timeout;

		/// <summary>
		///   The remaining time before the timeout is signaled. A value of -1 indicates that the timer is inactive.
		/// </summary>
		[Range(-1, 60, OverflowBehavior.Clamp)]
		private int _remainingTime = -1;

		/// <summary>
		///   Initializes a new instance.
		/// </summary>
		/// <param name="timeout">The timeout interval of the timer.</param>
		public Timer(int timeout)
		{
			_timeout = timeout;
			Y = 21;
		}

	

		/// <summary>
		///   Gets a value indicating whether the timeout has elapsed. This method returns true only for the single system step where
		///   the timeout occurs.
		/// </summary>
		public virtual bool HasElapsed() => _remainingTime == 0;

		/// <summary>
		///   Starts or restarts the timer.
		/// </summary>
		public virtual void Start() => _remainingTime = _timeout;

		public virtual void Start2()
		{
			_remainingTime = _timeout;
		}

		/// <summary>
		///   Stops the timer.
		/// </summary>
		public void Stop() => _remainingTime = -1;

		/// <summary>
		///   Gets a value indicating whether the timer is currently active, eventually signaling the timeout.
		/// </summary>
		public bool IsActive() => _remainingTime > 0;

		/// <summary>
		///   Gets the remaining time before the timeout occurs.
		/// </summary>
		public int GetRemainingTime() => _remainingTime;

		public virtual int X {get { return 1; }set { var i = 0;++i;
			}
		}

		protected virtual int Y { get; } = 99;

		protected virtual int Z => Y * 2;

		//private extern int W1 { get; }
		//private extern int W3 { set; }
		//private extern int W2 { get; set; }

		protected virtual int this[int i] => Y + Z;

		virtual protected int this[float i]
		{
			get { return 1; }
			set { X = value; }
		}

		protected virtual event Action E
		{
			add { X = 3; }
			remove { X = 7; }
		}

		private static void Main()
		{
			var t = new Timer(32);
			t.HasElapsed();
			t.Start();
			t.Start2();

			t.X = t.X;
			t.X = t.Y;
			t.X = t.Z;

			t[0.4f] = t[1] + t[1f];

			t.E += () => { };
			t.E -= () => { };
		}

		/// <summary>
		///   Updates the timer's internal state.
		/// </summary>
		public override void Update()
		{
			// TODO: Support different system step times
			--_remainingTime;

			if (_remainingTime < -1)
				_remainingTime = -1;
		}

//		}
//			public bool HasElapsed() => false;
//		{
//		public class SuppressTimeout : Fault
//		[Transient]
//		/// </summary>
//		///   Represents a failure mode that prevents the timer from reporting a timeout.

//		/// <summary>
	}
}