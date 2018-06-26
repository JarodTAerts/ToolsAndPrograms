package com.company;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.List;

public class GeneralUtils {

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
                    int currentLine=lines.indexOf(line);
                        reversedLines.add(lines.get(currentLine));
                }
            }
        }

        return reversedLines;
    }
}
