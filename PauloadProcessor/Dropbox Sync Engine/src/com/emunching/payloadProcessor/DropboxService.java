package com.emunching.payloadProcessor;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.List;
import java.util.Locale;
import java.util.logging.Level;
import java.util.logging.Logger;

import org.apache.commons.io.FileUtils;
import org.apache.commons.io.filefilter.DirectoryFileFilter;
import org.apache.commons.io.filefilter.RegexFileFilter;
import org.apache.commons.mail.DefaultAuthenticator;
import org.apache.commons.mail.HtmlEmail;

import com.dropbox.core.DbxClient;
import com.dropbox.core.DbxEntry;
import com.dropbox.core.DbxException;
import com.dropbox.core.DbxRequestConfig;

public class DropboxService {
	/**
	 * @param args
	 */
	
	private static Logger	 LOG	= Logger.getLogger(DropboxService.class.getName());
	private static String	 APP_KEY;
	private static String	 APP_SECRET;
	private static String	 ACCESS_TOKEN;
	private static String	 Local_Path;
	private static String	 DropBox_Path;
	private static String	 developerEmail;
	private static LogEngine	logger;
	
	public static void main(String[] args) throws IOException, DbxException {
		// Read Config variables from Config File (config.db in args[0] path or
		// current directory)
		File configFile = new File(args.length > 0 ? args[0] + "config.db" : "config.db");
		System.out.println("Config file: " + configFile.getAbsolutePath());
		if (configFile.exists()) {
			BufferedReader configFileReader = new BufferedReader(new FileReader(configFile));
			
			// load config variables to static variables
			DropboxService.APP_KEY = configFileReader.readLine();
			DropboxService.APP_SECRET = configFileReader.readLine();
			DropboxService.ACCESS_TOKEN = configFileReader.readLine();
			DropboxService.developerEmail = configFileReader.readLine();
			DropboxService.Local_Path = configFileReader.readLine();
			DropboxService.DropBox_Path = configFileReader.readLine();
			// initialize log engine
			DropboxService.logger = new LogEngine(configFileReader.readLine(), DropboxService.developerEmail);
				
			configFileReader.close();
			// start dropbox clinet and sync engine
			DropboxService syncEngine = new DropboxService();
			// syncEngine.sync_folder_dropbox_to_local(DropboxService.DropBox_Path);
			syncEngine.sync_files_dropbox_to_local();
		} else {
			// if config file not exists, throw error and send email to
			// developer
			System.out.println("Configuration File not found. Please create a config.db file in current directory with read / write access");
			System.exit(0);
		}
		
	}
	
	private final DbxClient	dropBoxClient;
	
	public DropboxService() throws IOException, DbxException {
		
		DbxRequestConfig config = new DbxRequestConfig("ToitPayload/1.0", Locale.getDefault().toString());
		dropBoxClient = new DbxClient(config, DropboxService.ACCESS_TOKEN);
	}
	
	private void downloadFile(String FileName) throws FileNotFoundException, DbxException, IOException {
		FileOutputStream outputStream = new FileOutputStream(DropboxService.Local_Path + FileName);
		try {
			DropboxService.logger.log(false, "Downloading file: " + FileName + " => " + DropboxService.Local_Path + FileName);
			DbxEntry.File downloadedFile = dropBoxClient.getFile(FileName, null, outputStream);
			DropboxService.LOG.log(Level.INFO, "Downloaded: {0}", downloadedFile.toString());
		} catch (Exception e) {
			handleException(e);
		} finally {
			outputStream.close();
			DropboxService.LOG.log(Level.INFO, "Download finished");
			DropboxService.logger.log(false, "Download finished");
		}
	}
	
	private List<String> getNamesFromDropboxEntryCollection(List<DbxEntry> dbxFiles) {
		ArrayList<String> paths = new ArrayList<String>();
		for (DbxEntry element : dbxFiles) {
			DbxEntry dbxEntry = element;
			if (dbxEntry.isFile()) paths.add(dbxEntry.name);
		}
		return paths;
	}
	
	private List<String> getNamesFromFilesCollection(Collection<File> files) {
		ArrayList<String> paths = new ArrayList<String>();
		for (Object element : files) {
			File file = (File) element;
			paths.add(file.getName());
		}
		return paths;
	}
	
	private void handleException(Exception e) {
		e.printStackTrace();
		StringWriter sw = new StringWriter();
		PrintWriter pw = new PrintWriter(sw);
		e.printStackTrace(pw);
		
		DropboxService.logger.log(true, "Caught Exception at " + new SimpleDateFormat("dd/MM/yyyy HH:mm:ss").format(new Date()) + "<br/><br/>" + sw.toString());
	}
	
	private Collection<File> listFilesRecusively(File dir) {
		Collection<File> files = FileUtils.listFiles(
		        dir,
		        new RegexFileFilter("^(.*?)"),
		        DirectoryFileFilter.DIRECTORY
		        );
		return files;
	}
	
	private void sync_files_dropbox_to_local() {
		try {
			// get all file names into array from dropbox recursively
			DbxEntry.WithChildren dropbox_files = dropBoxClient.getMetadataWithChildren(DropboxService.DropBox_Path);
			// System.out.println(dropbox_files.toString());
			List<String> dbx_file_names = getNamesFromDropboxEntryCollection(dropbox_files.children);
			// get all file names into array from local recursively
			Collection<File> local_files = listFilesRecusively(new File(DropboxService.Local_Path + DropboxService.DropBox_Path));
			List<String> local_file_names = getNamesFromFilesCollection(local_files);
			// subtract local from dropbox files
			dbx_file_names.removeAll(local_file_names);
			// get list of files from dropbox which are not in local and
			// download them
			for (String file_Name_to_be_downloaded : dbx_file_names)
				if (file_Name_to_be_downloaded != null) {
					DropboxService.logger.log(false, "Requesting Download: " + file_Name_to_be_downloaded);
					downloadFile(DropboxService.DropBox_Path + "/" + file_Name_to_be_downloaded);
				} else DropboxService.logger.log(false, "Trying to process folder which is not allowed");
		} catch (DbxException e) {
			handleException(e);
		} catch (FileNotFoundException e) {
			handleException(e);
		} catch (IOException e) {
			handleException(e);
		} catch (Exception e) {
			handleException(e);
		}
		
	}
}

class LogEngine {
	private final File	 logFile;
	private FileWriter	 logFileWriter;
	private final String	emailAddesss;
	
	public LogEngine(String FilePath, String developerEmail) {
		logFile = new File(FilePath);
		emailAddesss = developerEmail;
	}
	
	public boolean log(boolean isError, String message) {
		if (isError) {
			// send email and write to file
			sendEmail(message);
			WriteToFile(message);
			return true;
		} else {// write to file
			WriteToFile(message);
			return true;
		}
	}
	
	private boolean sendEmail(String htmlString) {
		try {
			HtmlEmail email = new HtmlEmail();
			email.setHostName("smtp.gmail.com");
			email.setSmtpPort(465);
			email.setAuthenticator(new DefaultAuthenticator("tech@emunching.com", "..emunching01"));
			email.setSSLOnConnect(true);
			email.setFrom("tech@emunching.com");
			email.setSubject("Notice from Dropbox Sync Engine");
			email.setHtmlMsg("<html>\n" +
			        "<body>\n" +
			        "\n" +
			        htmlString.replaceAll("\n", "<br/>") +
			        "\n" +
			        "</body>\n" +
			        "</html>");
			// set the alternative message
			email.setTextMsg("Notice from Dropbox Sync Engine");
			email.addTo(emailAddesss.split(","));
			email.send();
			System.out.println("Email Sent");
			return true;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return false;
	}
	
	private boolean WriteToFile(String message) {
		if (logFile.exists()) try {
			logFileWriter = new FileWriter(logFile, true);
			logFileWriter.write("\n" + new SimpleDateFormat("dd/MM/yyyy HH:mm:ss").format(new Date()) + "\n" + message);
			logFileWriter.close();
			return true;
		} catch (IOException e) {
			e.printStackTrace();
			return false;
		}
		else try {
			if (logFile.createNewFile()) WriteToFile(message);
			return true;
		} catch (IOException e) {
			e.printStackTrace();
			return true;
		}
		
	}
}
