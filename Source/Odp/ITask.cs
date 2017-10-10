﻿// The MIT License (MIT)
// 
// Copyright (c) 2014-2016, Institute for Software & Systems Engineering
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

namespace SafetySharp.Odp
{
	using JetBrains.Annotations;

	/// <summary>
	///  A task describes how resources are processed by the self-organizing system.
	/// </summary>
	public interface ITask
	{
		/// <summary>
		///  The sequence of capabilities that must be applied to resources.
		/// </summary>
		[NotNull]
		ICapability[] RequiredCapabilities { get; }

		/// <summary>
		///  Whether or not processing for this task is complete.
		///  For instance, implementations may always return <c>false</c> to indicate a never-ending task,
		///  or may define a fixed number of resources to be produced and return <c>true</c> after that number is reached.
		/// </summary>
		bool IsCompleted { get; }
	}
}
