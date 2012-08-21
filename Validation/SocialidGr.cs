#region license
/* 
 * SocialidGr - Version 1.0
 * Copyright (c) 2012 - Petros Kyladitis
 * All rights reserved.
 * 
 * This class provide validation checking for the greek social id, knwon as AMKA.
 * It checks only if the social id is valid, not if exists and corresponds to a 
 * natural person.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met: 
 * 
 * 1. Redistributions of source code must retain the above copyright notice, this
 *    list of conditions and the following disclaimer. 
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution. 
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
 #endregion
 
using System ;
using System.Collections.Generic ;
using System.Globalization ;

namespace Multipetros.Validation{
	
	/// <summary>
 	/// This class provide static methods for validation checking of the greek social id, knwon as AMKA,
 	/// AMKA have 11 digits. The first 6 represent the birthdate of the owner.
  	/// The last digit is validation check digit as implemented by the Lunh formula.
  	/// Notice: This class checks only if the social id is valid, not if exists and corresponds to a natural person.
	/// </summary>
	public static class SocialidGr{

		/// <summary>
		/// The first 6 digits of greek social id represents the owner's birth date.
		/// This method checks is social id starts with a valid date.
		/// </summary>
		/// <param name="sidNum">The social id number (or the 1st 6 digits), in string</param>
		/// <returns>True if date part validation passed, else false</returns>
		private static bool DateValidate(string sidNum){
			//the provided social id number must have at least 6 digits (if is only the date part)
			if(sidNum.Length < 6)
				return false ;			
			
			string sidDate = sidNum.Substring(0, 6) ; //get the first six digits of the social id			
			string dateFormat = "ddMMyy" ; //etc. 090212 for Feb-09-2012
			DateTime dateTemp ; //temporary value, needed for DateTime.TryParseExact functionality
			
			return DateTime.TryParseExact(sidDate, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTemp) ;
			                      
		}
				
		/// <summary>
		/// The greek social id number have 11 digts and use's the Luhn formula. 
		/// The first 6 digits represent the owner's date of birth.<br />
		/// This method check the social id for both, valid birth date and Luhn format.
		/// </summary>
		/// <param name="sidNum">Social id number</param>
		/// <returns>True if is valid, else false.</returns>
		public static bool IsValid(string sidNum){
			//if id have less or more than 11 digits is not valid
			if(sidNum.Length != 11)
				return false ;
			
			//validate at first for date & if ok, for Luhn form
			if(DateValidate(sidNum))
				return Luhn.IsValid(sidNum) ;
			return false ;
		}
	}
}