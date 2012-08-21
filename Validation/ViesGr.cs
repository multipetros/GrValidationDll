#region license
/* 
 * ViesGr - Version 1.1
 * Copyright (C) 2011 Petros Kyladitis
 * A greek VIES VAT number validation class.
 * 
 * The classes methods check if a VIES VAT number is valid, and
 * NOT if exists and corresponds to a natural person or a legal entity.
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

namespace Multipetros.Validation{
        
        /// <summary>
        /// The class method check if a VIES VAT number is valid
        /// </summary>
        public static class ViesGr{
                
                /// <summary>
                /// Static method for VIES VAT ID validation checking
                /// </summary>
                /// <param name="viesVatId">The VIES VAT ID to check</param>
                /// <returns>true if the VIES VAT ID is valid, false if it's invalid</returns>
                public static bool IsValid(int viesVatId){
                        
                        // all GR VIES IDs has 9 digits and starts counting from 100000000
                        if(viesVatId > 999999999 || viesVatId < 9999999) 
                                return false ; 
                        
                        //deploy the 9 digits of the number to a table,
                        //beginning from LSD at last table position [8] => to MSD at first table position [0]
                        int digit ;
                        byte[] digiTable = new byte[9] ;        
                        for(int i=digiTable.Length-1; i>=0; i--){
                                digit = viesVatId%10;
                                viesVatId = (viesVatId-digit)/10;
                                digiTable[i]=(byte)digit;
                        }
                        
                        //multiply all nums in table[0 to 7] by 2^(8-n) [n = table position]
                        //and summarize the products
                        int multiplier = 256;
                        int sum = 0;                    
                        for(int i=0; i<digiTable.Length-1; i++){
                                sum+=digiTable[i]*multiplier;
                                multiplier/=2;
                        }
                        
                        //if the modulus of the sumarized products by 11 (a num from 0 to 10; if 10 set it to 0) 
                        //equals the last digit of the id (table last position [8]), the id is valid
                        sum%=11;
                        if(sum==10) sum=0;                      
                        if(sum!=digiTable[8])
                                return false;
                        return true;
                }
                
                /// <summary>
                /// Static method for string VIES VAT ID validation checking
                /// </summary>
                /// <param name="viesVatId">The VIES VAT ID to check</param>
                /// <returns>true if the VIES VAT ID is valid, false if it's invalid</returns>
                public static bool IsValid(string viesVatId){
                	int viesNum ;
                	if(int.TryParse(viesVatId, out viesNum))
                		return IsValid(viesNum) ;
                	else
                		return false ;
                }
        }
}
