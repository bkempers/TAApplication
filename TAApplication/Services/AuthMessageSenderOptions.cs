/**
 * Author:      Trevor Koenig
 * Partner:     Ben Kempers
 * Date:        9/27/2022
 * Course:      CS 4540, University of Utah, School of Computing
 * Copyright:   CS 4540 and [Ben Kempers and Trevor Koenig] - This work may not be copied for use in Academic Coursework.
 *
 * I, Trevor Koenig and Ben Kempers, certify that I wrote this code from scratch and did 
 * not copy it in part or whole from another source.  Any references used 
 * in the completion of the assignment are cited in my README file and in
 * the appropriate method header.
 *
 *    Acts as a Authorized Message Sender wrapper
 *    
 */

namespace WebPWrecover.Services;

public class AuthMessageSenderOptions
{
    public string? SendGridKey { get; set; }
}