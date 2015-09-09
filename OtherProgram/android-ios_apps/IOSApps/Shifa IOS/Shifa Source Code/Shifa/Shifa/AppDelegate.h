//
//  AppDelegate.h
//  Shifa
//
//  Created by Mac Mini on 4/18/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "FMDatabase.h"
BOOL isInternetAvailable;

@interface AppDelegate : UIResponder <UIApplicationDelegate>
{
    
    
    
    
    
    
}
+(void)networkCheck;
@property (strong, nonatomic) UIWindow        *window;
@property (nonatomic, retain) FMDatabase      * database;

//Setting
@property (strong , nonatomic) NSString       *strUserSelectedLanguage;



//Kent
@property (strong , nonatomic) NSString       *strSelectedRowStringAtKentVC1;
@property (strong , nonatomic) NSString       *strPassStringKentVC2toPadVC;
@property (strong , nonatomic) NSString       *strPassStringKentFavoriteToDetailRemedies;
@property (strong , nonatomic) NSMutableArray *arrCheckBoxClikedId;


//Material Medica
@property (strong , nonatomic)NSString        *strSelectedRowStringAtMaterialMedicaVc1;
@property (strong , nonatomic)NSString        *strSelectedRowStringAtMaterialMedicaVc2;

@end
