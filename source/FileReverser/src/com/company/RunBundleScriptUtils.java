package com.company;


import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class RunBundleScriptUtils {

    /**
     * Function to reverse a run bundle script as formatted by me when doing flair research
     * This will not reverse normal files
     * @param inputFile path of the input file
     * @param outputFile path of the output file
     * @return
     */
    public static boolean ReverseBundleScript(String inputFile,String outputFile){
        try {

            GeneralUtils.PrintToFile(outputFile,ReverseBundleScriptLines(GeneralUtils.ReadLines(inputFile)));
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
    public static void ReverseAndSwitchToRemoveCommand(String inputFile,String outputFile, int bundleInt){
        ReverseBundleScript(inputFile,outputFile);
        CopyToRemoveCommandForFile(outputFile,outputFile,bundleInt);
    }

    /**
     * Function to flip the lines of a file using the format of the bundle shell scripts
     * @param lines List of lines from the original file
     * @return
     */
    public static List<String> ReverseBundleScriptLines(List<String> lines){

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
     * Function that takes an input run bundle script file and converts all the cp commands to rm commands
     * @param inputFile path of the file to be processed
     * @param outputFile path of where the processed file will be saved
     * @param bundleInt int representing what bundle of apks this file deals with
     */
    public static void CopyToRemoveCommandForFile(String inputFile,String outputFile, int bundleInt){

        try {
            List<String> lines = GeneralUtils.ReadLines(inputFile);
            for(String line:lines){
                if(line.startsWith("cp")){
                    String newLine=CopyToRemoveCommandForLine(line,bundleInt);
                    lines.set(lines.indexOf(line),newLine);
                }
            }

            GeneralUtils.PrintToFile(outputFile,lines);
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
    public static String CopyToRemoveCommandForLine(String line, int apkBundle){
        String startString="apk"+apkBundle;
        if(apkBundle==3)
            startString="akp"+apkBundle;
        String endString="./";

        int startIndex=line.indexOf(startString);
        int endIndex=line.indexOf(endString);

        String apkName=line.substring(startIndex+4,endIndex);
        String path=line.substring(endIndex);

        String newLine="rm "+path+apkName;

        return newLine;
    }

}
