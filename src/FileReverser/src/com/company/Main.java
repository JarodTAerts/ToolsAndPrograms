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
                ReverseRunBundleScript(GetInputFileName(reader),GetOutputFileName(reader));
            }
            if(nextCommand.equals("help")){
                PrintHelp();
            }
            if(nextCommand.equals("cpToRm")){

                CpToRmFile(GetInputFileName(reader),GetOutputFileName(reader),GetBundleInt(reader));
            }
            if(nextCommand.equals("Rev&Rm")){
                ReverseAndRmScript(GetInputFileName(reader),GetOutputFileName(reader),GetBundleInt(reader));
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
        System.out.println("Rev&Rm\t\t|\t\tBoth Reverses a file and converts it to run decreasing. Combination of revRBS and cpToRm");

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

    /**
     * Function to reverse a run bundle script as formatted by me when doing flair research
     * This will not reverse normal files
     * @param inputFile path of the input file
     * @param outputFile path of the output file
     * @return
     */
    public static boolean ReverseRunBundleScript(String inputFile,String outputFile){
        try {

            PrintToFile(outputFile,ReverseLines(ReadLines(inputFile)));
            System.out.println("Reverse successful! Output contained in: "+outputFile);
            return true;
        }catch( IOException e){
            System.out.println("Reverse Failed due to a problem. Sorry there is no more information. If you are using this program you are probably Jarod." +
                    "Just look at the source and figure it out.");
            return false;
        }
    }

    /**
     * Function to both reverse a run bundle script and change all the cp commands to rm commands for the apk files
     * @param inputFile path of the input file
     * @param outputFile path of the output file
     * @param bundleInt int that is what bundle of apks this script deals with
     */
    public static void ReverseAndRmScript(String inputFile,String outputFile, int bundleInt){
        ReverseRunBundleScript(inputFile,outputFile);
        CpToRmFile(outputFile,outputFile,bundleInt);
    }

    /**
     * Function to read all the lines from a file and put them into a List of strings
     * @param filename path of the file to be read into the list
     * @return
     * @throws IOException
     */
    public static List<String> ReadLines(String filename) throws IOException {
        FileReader fileReader=new FileReader(filename);
        BufferedReader bufferedReader=new BufferedReader(fileReader);
        List<String> lines=new ArrayList<>();
        String line=null;

        while((line=bufferedReader.readLine())!=null){
            lines.add(line);
        }
        bufferedReader.close();
        fileReader.close();

        return lines;
    }

    /**
     * Function to flip the lines of a file using the format of the bundle shell scripts
     * @param lines List of lines from the original file
     * @return
     */
    public static List<String> ReverseLines(List<String> lines){

        List<String> reversedLines=new ArrayList<>();
        for(int i=50;i>0;i--) {
            for (String line : lines) {
                if (line.equals("#"+i)){
                    int start=lines.indexOf(line);
                    for(int j=start;j<start+4;j++) {
                        reversedLines.add(lines.get(j));
                    }
                }
            }
        }

        return reversedLines;
    }

    /**
     * Function to print a list of string to an output file
     * @param outputFile path to output file
     * @param lines list of string to be printed to an output file
     * @throws IOException
     */
    public static void PrintToFile(String outputFile, List<String> lines) throws IOException{
        PrintWriter writer=new PrintWriter(outputFile);
        for(String line:lines){
            writer.println(line);
        }
        writer.close();

        return;
    }

    /**
     * Function that takes an input run bundle script file and converts all the cp commands to rm commands
     * @param inputFile path of the file to be processed
     * @param outputFile path of where the processed file will be saved
     * @param bundleInt int representing what bundle of apks this file deals with
     */
    public static void CpToRmFile(String inputFile,String outputFile, int bundleInt){

        try {
            List<String> lines = ReadLines(inputFile);
            for(String line:lines){
                if(line.startsWith("cp")){
                    String newLine=CpToRmLine(line,bundleInt);
                    lines.set(lines.indexOf(line),newLine);
                }
            }

            PrintToFile(outputFile,lines);
            System.out.println("Reverse successful! Output contained in: "+outputFile);
        }catch(IOException e){
            System.out.println("Cp To Rm failed. Sorry there is no more information. If you are using this program you are probably Jarod." +
                    "Just look at the source and figure it out.");
        }
    }

    /**
     * Function that takes a line with a cp commands and converts it to a line with an rm commands on the same apk file
     * @param line string of the line with the cp command
     * @param apkBundle int of the bundle where the apk the command deals with is
     * @return
     */
    public static String CpToRmLine(String line, int apkBundle){
        String startString="apk"+apkBundle;
        String endString="./";

        int startIndex=line.indexOf(startString);
        int endIndex=line.indexOf(endString);

        String apkName=line.substring(startIndex+4,endIndex);
        String path=line.substring(endIndex);

        String newLine="rm "+path+apkName;

        return newLine;
    }
}
