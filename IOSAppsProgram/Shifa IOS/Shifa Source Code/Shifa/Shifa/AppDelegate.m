//
//  AppDelegate.m
//  Shifa
//
//  Created by Mac Mini on 4/18/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "AppDelegate.h"
#import "Reachability.h"

@implementation AppDelegate
@synthesize strUserSelectedLanguage,strSelectedRowStringAtKentVC1,arrCheckBoxClikedId,strPassStringKentVC2toPadVC,strSelectedRowStringAtMaterialMedicaVc1,strSelectedRowStringAtMaterialMedicaVc2,strPassStringKentFavoriteToDetailRemedies;

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{

    //Write database
    [self copyDatabaseIfNeeded];
    
    //Initialize FMDB
    _database=[FMDatabase databaseWithPath:[self getDBPath]];
    [_database open];
  
   
    
    [[UIApplication sharedApplication] setStatusBarStyle:UIStatusBarStyleLightContent];
    //Select Default Language As English
    self.strUserSelectedLanguage = @"";

    
    arrCheckBoxClikedId = [[NSMutableArray alloc]init];
    // show in the status bar that network activity is starting
    //[UIApplication sharedApplication].networkActivityIndicatorVisible = YES;

    
    return YES;
}
- (void) copyDatabaseIfNeeded {
    
    //Using NSFileManager we can perform many file system operations.
    NSFileManager *fileManager = [NSFileManager defaultManager];
    NSError *error;
    
    NSString *dbPath = [self getDBPath];
    BOOL success = [fileManager fileExistsAtPath:dbPath];
    
    if(!success) {
        
        NSString *defaultDBPath = [[[NSBundle mainBundle] resourcePath] stringByAppendingPathComponent:@"shifa.sqlite"];
        success = [fileManager copyItemAtPath:defaultDBPath toPath:dbPath error:&error];
        
        
        //Exclude DB Frome Backup At iCloud
        NSURL * fileURL;
        fileURL = [ NSURL fileURLWithPath: defaultDBPath];
        if ( [ fileURL setResourceValue: [ NSNumber numberWithBool: YES ] forKey: NSURLIsExcludedFromBackupKey error: nil ]){
            // //DLog(@"DataBase Not Backedup to iCloud");
        }else{
            ////DLog(@"Error copying the file");
        }

        
        if (!success)
            NSAssert1(0, @"Failed to create writable database file with message '%@'.", [error localizedDescription]);
    }
}

- (NSString *) getDBPath
{
    NSArray *paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory , NSUserDomainMask, YES);
    NSString *documentsDir = [paths objectAtIndex:0];
    DLog(@"dbpath : %@",documentsDir);
    return [documentsDir stringByAppendingPathComponent:@"shifa.sqlite"];
}


+(void)networkCheck
{
    Reachability *curReach = [Reachability reachabilityForInternetConnection] ;
    NetworkStatus netStatus = [curReach currentReachabilityStatus];
    switch (netStatus)
    {
        case NotReachable:
        {
            UIAlertView *connectionAlert = [[UIAlertView alloc] init];
            [connectionAlert setTitle:nil];
            [connectionAlert setMessage:@"A connection error occurred.\nCheck your network settings and try again."];
            [connectionAlert setDelegate:self];
            [connectionAlert setTag:1];
            [connectionAlert addButtonWithTitle:@"OK"];
            [connectionAlert show];
            // NSLog(@"NETWORKCHECK: Not Connected");
            isInternetAvailable=NO;
            break;
        }
        case ReachableViaWWAN:
        {
            //  NSLog(@"NETWORKCHECK: Connected Via WWAN");
            isInternetAvailable=YES;
            break;
        }
        case ReachableViaWiFi:
        {
            // NSLog(@"NETWORKCHECK: Connected Via WiFi");
            isInternetAvailable=YES;
            break;
        }
            
            
    }
}

- (void)applicationWillResignActive:(UIApplication *)application
{
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
}

- (void)applicationDidEnterBackground:(UIApplication *)application
{
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
}

- (void)applicationWillEnterForeground:(UIApplication *)application
{
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
}

- (void)applicationDidBecomeActive:(UIApplication *)application
{
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
}

- (void)applicationWillTerminate:(UIApplication *)application
{
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
}

@end
