package com.company;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Main {

    public static void main(String[] args) {
        String nextCommand="";
        Scanner reader=new Scanner(System.in);
        System.out.println("Welcome to Jarod's Java File Utils. Use 'help' to get help.");
        while(!nextCommand.equals("exit")){
            if(nextCommand.equals("revRBS")){
                RunBundleScriptUtils.ReverseBundleScript(GetInputFileName(reader),GetOutputFileName(reader));
            }
            if(nextCommand.equals("help")){
                PrintHelp();
            }
            if(nextCommand.equals("cpToRm")){

                RunBundleScriptUtils.CopyToRemoveCommandForFile(GetInputFileName(reader),GetOutputFileName(reader),GetBundleInt(reader));
            }
            if(nextCommand.equals("rev&Rm")){
                RunBundleScriptUtils.ReverseAndSwitchToRemoveCommand(GetInputFileName(reader),GetOutputFileName(reader),GetBundleInt(reader));
            }else if(nextCommand.equals("revFil")){
                GeneralUtils.ReverseLinesOfFile(GetInputFileName(reader),GetOutputFileName(reader));
            }

            System.out.print("Enter command: ");
            nextCommand=reader.nextLine();
        }

        System.out.println("Exiting Program...");
        System.exit(0);
    }

    /**
     * Function to print out help for all of the functions in this program
     */
    public static void PrintHelp(){
        System.out.println("Command\t\t|\t\tDescription");
        System.out.println("-------\t\t|\t\t-----------");
        System.out.println("revRBS\t\t|\t\tReverse a RunBundleScript file for Flair testing");
        System.out.println("help\t\t|\t\tShows this page");
        System.out.println("exit\t\t|\t\tExits the program");
        System.out.println("cpToRm\t\t|\t\tConverts a RunBundle Shell script from increasing apps to decreasing them.");
        System.out.println("rev&Rm\t\t|\t\tBoth Reverses a file and converts it to run decreasing. Combination of revRBS and cpToRm");
        System.out.println("revFil\t\t|\t\tReverse the lines of any file");


    }

    /**
     * Function to get input from the user as to what bundle the file they are processing deals with
     * @param reader reader to get input from the user
     * @return
     */
    public static int GetBundleInt(Scanner reader){
        System.out.print("Enter the number of the bundle this script runs: ");
        int bundleInt=reader.nextInt();
        return bundleInt;
    }

    /**
     * Function to get the path of the input file to be processed
     * @param reader reader to get input from the user
     * @return
     */
    public static String GetInputFileName(Scanner reader){
        System.out.print("What is the absolute path of the input file: ");
        String inputFile=reader.nextLine();
        return inputFile;
    }

    /**
     * Function to get the output file where the processed file will be saved, can be the same as the input file
     * @param reader reader to get input from the user
     * @return
     */
    public static String GetOutputFileName(Scanner reader){
        System.out.print("What is the absolute path of the output file: ");
        String outputFile=reader.nextLine();
        return outputFile;
    }


}
