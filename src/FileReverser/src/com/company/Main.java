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

    public static void PrintHelp(){
        System.out.println("Command\t\t|\t\tDescription");
        System.out.println("-------\t\t|\t\t-----------");
        System.out.println("revRBS\t\t|\t\tReverse a RunBundleScript file for Flair testing");
        System.out.println("help\t\t|\t\tShows this page");
        System.out.println("exit\t\t|\t\tExits the program");
        System.out.println("cpToRm\t\t|\t\tConverts a RunBundle Shell script from increasing apps to decreasing them.");
        System.out.println("Rev&Rm\t\t|\t\tBoth Reverses a file and converts it to run decreasing. Combination of revRBS and cpToRm");

    }

    public static int GetBundleInt(Scanner reader){
        System.out.print("Enter the number of the bundle this script runs: ");
        int bundleInt=reader.nextInt();
        return bundleInt;
    }

    public static String GetInputFileName(Scanner reader){
        System.out.print("What is the absolute path of the input file: ");
        String inputFile=reader.nextLine();
        return inputFile;
    }

    public static String GetOutputFileName(Scanner reader){
        System.out.print("What is the absolute path of the output file: ");
        String outputFile=reader.nextLine();
        return outputFile;
    }

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

    public static void ReverseAndRmScript(String inputFile,String outputFile, int bundleInt){
        ReverseRunBundleScript(inputFile,outputFile);
        CpToRmFile(outputFile,outputFile,bundleInt);
    }

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

    public static void PrintToFile(String outputFile, List<String> lines) throws IOException{
        PrintWriter writer=new PrintWriter(outputFile);
        for(String line:lines){
            writer.println(line);
        }
        writer.close();

        return;
    }

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
