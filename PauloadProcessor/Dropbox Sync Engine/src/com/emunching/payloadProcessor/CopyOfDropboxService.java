package com.emunching.payloadProcessor;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Locale;
import java.util.logging.Level;
import java.util.logging.Logger;

import org.apache.commons.io.FileUtils;
import org.apache.commons.io.filefilter.DirectoryFileFilter;
import org.apache.commons.io.filefilter.RegexFileFilter;

import com.dropbox.core.DbxClient;
import com.dropbox.core.DbxEntry;
import com.dropbox.core.DbxException;
import com.dropbox.core.DbxRequestConfig;
import com.dropbox.core.DbxWriteMode;

public class CopyOfDropboxService {
	/**
	 * @param args
	 */
	
	private static Logger	LOG	= Logger.getLogger(CopyOfDropboxService.class.getName());
	private static String	APP_KEY;
	private static String	APP_SECRET;
	private static String	ACCESS_TOKEN;
	private static String	Local_Path;
	private static String	DropBox_Path;
	private static String	developerEmail;
	
	public static void main(String[] args) throws IOException, DbxException {
		// Read Config variables from Config File (config.db in current
		// directory)
		File configFile = new File(args.length > 0 ? args[0] + "config.db" : "config.db");
		System.out.println("Config file: " + configFile.getAbsolutePath());
		if (configFile.exists()) {
			BufferedReader configFileReader = new BufferedReader(new FileReader(configFile));
			
			// load config variables to static variables
			CopyOfDropboxService.APP_KEY = configFileReader.readLine();// "no41blb9ajruo2g";
			CopyOfDropboxService.APP_SECRET = configFileReader.readLine();// "h3hxzvreckm6gzr";
			CopyOfDropboxService.ACCESS_TOKEN = configFileReader.readLine();// "5jH2aHoVmjYAAAAAAAAAAb4kyz-cc-rewPplSzmpFH2dNHHnYNg3KwJooDPYy3K-";
			CopyOfDropboxService.Local_Path = configFileReader.readLine();// "F:/Dropbox/";
			CopyOfDropboxService.DropBox_Path = configFileReader.readLine();// "/Test";
			CopyOfDropboxService.developerEmail = configFileReader.readLine();
			
			// start dropbox clinet and sync engine
			CopyOfDropboxService syncEngine = new CopyOfDropboxService();
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
	
	public CopyOfDropboxService() throws IOException, DbxException {
		
		DbxRequestConfig config = new DbxRequestConfig("ToitPayload/1.0", Locale.getDefault().toString());
		dropBoxClient = new DbxClient(config, CopyOfDropboxService.ACCESS_TOKEN);
		
		/*
		 * DbxAppInfo appInfo = new DbxAppInfo(DropboxService.APP_KEY,
		 * DropboxService.APP_SECRET);
		 * DbxRequestConfig config = new DbxRequestConfig("JavaTutorial/1.0",
		 * Locale.getDefault().toString());
		 * DbxWebAuthNoRedirect webAuth = new DbxWebAuthNoRedirect(config,
		 * appInfo);
		 * // Have the user sign in and authorize your app.
		 * String authorizeUrl = webAuth.start();
		 * System.out.println("1. Go to: " + authorizeUrl);
		 * System.out.println("2. Click Allow (you might have to log in first)");
		 * System.out.println("3. Copy the authorization code.");
		 * String code = new BufferedReader(new
		 * InputStreamReader(System.in)).readLine().trim();
		 * // This will fail if the user enters an invalid authorization code.
		 * DbxAuthFinish authFinish = webAuth.finish(code);
		 * client = new DbxClient(config, authFinish.accessToken);
		 * System.out.println("Linked account: " +
		 * client.getAccountInfo().displayName);
		 * System.out.println("Access Token: " + authFinish.accessToken);
		 */
		
	}
	
	private void downloadFile(String FileName) throws FileNotFoundException, DbxException, IOException {
		FileOutputStream outputStream = new FileOutputStream(CopyOfDropboxService.Local_Path + FileName);
		try {
			System.out.println("Downloading file: " + FileName + " => " + CopyOfDropboxService.Local_Path + FileName);
			DbxEntry.File downloadedFile = dropBoxClient.getFile(FileName, null, outputStream);
			// System.out.println("Metadata: " + downloadedFile.toString());
			CopyOfDropboxService.LOG.log(Level.INFO, "Downloaded: {0}", downloadedFile.toString());
		} finally {
			outputStream.close();
			CopyOfDropboxService.LOG.log(Level.INFO, "Download finished");
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
	
	private Collection<File> listFilesRecusively(File dir) {
		Collection<File> files = FileUtils.listFiles(
		        dir,
		        new RegexFileFilter("^(.*?)"),
		        DirectoryFileFilter.DIRECTORY
		        );
		return files;
	}
	
	private void mkdir(String folder_name) {
		File f = new File(CopyOfDropboxService.Local_Path + folder_name);
		if (!f.exists()) f.mkdir();
		else System.out.println("Fodler Alrady Exists");
	}
	
	private void sync_files_dropbox_to_local() {
		try {
			// get all file names into array from dropbox recursively
			DbxEntry.WithChildren dropbox_files = dropBoxClient.getMetadataWithChildren(CopyOfDropboxService.DropBox_Path);
			System.out.println(dropbox_files.toString());
			List<String> dbx_file_names = getNamesFromDropboxEntryCollection(dropbox_files.children);
			System.out.println(dbx_file_names.toString());
			// get all file names into array from local recursively
			System.out.println(CopyOfDropboxService.Local_Path + CopyOfDropboxService.DropBox_Path);
			Collection<File> local_files = listFilesRecusively(new File(CopyOfDropboxService.Local_Path + CopyOfDropboxService.DropBox_Path));
			List<String> local_file_names = getNamesFromFilesCollection(local_files);
			System.out.println(local_file_names.toString());
			// subtract local from dropbox files
			dbx_file_names.removeAll(local_file_names);
			System.out.println("DBox Files to be downloaded: " + dbx_file_names.toString());
			// get list of files from dropbox which are not in local and
			// download them
			for (String element : dbx_file_names) {
				String file_Name_to_be_downloaded = element;
				if (file_Name_to_be_downloaded != null) {
					System.out.println("Requesting Download: " + file_Name_to_be_downloaded);
					downloadFile(CopyOfDropboxService.DropBox_Path + "/" + file_Name_to_be_downloaded);
				}
			}
		} catch (DbxException e) {
			e.printStackTrace();
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		
	}
	
	private void sync_folder_dropbox_to_local(String scan_path) throws DbxException {
		DbxEntry.WithChildren listing = dropBoxClient.getMetadataWithChildren(scan_path);
		System.out.println("Files in the root path: " + scan_path);
		for (DbxEntry child : listing.children) {
			File file = new File(CopyOfDropboxService.Local_Path + scan_path + "/" + child.name);
			if (child.isFile()) {
				if (!file.exists()) try {
					downloadFile(scan_path + "/" + child.name);
				} catch (FileNotFoundException e) {
					e.printStackTrace();
				} catch (IOException e) {
					e.printStackTrace();
				}
				else System.out.println("File Exists: " + child.name);
			} else if (child.isFolder()) {
				System.out.println(child.path);
				if (!file.exists()) mkdir(child.path);
				sync_folder_dropbox_to_local(child.path);
			}
		}
	}
	
	public void uploadFile(String filename) throws IOException, DbxException {
		File inputFile = new File(filename);
		FileInputStream inputStream = new FileInputStream(inputFile);
		try {
			DbxEntry.File uploadedFile = dropBoxClient.uploadFile("/magnum-opus.txt", DbxWriteMode.add(), inputFile.length(), inputStream);
			System.out.println("Uploaded: " + uploadedFile.toString());
			CopyOfDropboxService.LOG.log(Level.INFO, "Uploaded: {0}", uploadedFile.toString());
		} finally {
			inputStream.close();
			CopyOfDropboxService.LOG.log(Level.INFO, "Upload finished");
		}
	}
}
