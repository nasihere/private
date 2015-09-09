//
//  MICheckBox.m
//  aaa
//
//  Created by apppocraze on 7/5/13.
//  Copyright (c) 2013 apppocraze. All rights reserved.
//

#import "MICheckBox.h"


@implementation MICheckBox
@synthesize isChecked;


- (id)initWithFrame:(CGRect)frame
{
    
    self = [super initWithFrame:frame];
    
    if(self){
        
    app = (AppDelegate *)[UIApplication sharedApplication].delegate;
        
       self.contentHorizontalAlignment = UIControlContentHorizontalAlignmentLeft;
       [self setImage:[UIImage imageNamed:@"cb_glossy_off.png"] forState:UIControlStateNormal];
       [self addTarget:self action:@selector(checkBoxClicked:)forControlEvents:UIControlEventTouchUpInside];
    
   }
    return self;

      
    
}




-(void)checkBoxClicked:(id)tt{
    
   
    MICheckBox *ss = (MICheckBox *)tt;
    

    if(self.isChecked == NO){
        self.isChecked =YES;
        [ss setImage:[UIImage imageNamed:@"cb_glossy_on.png"]forState:UIControlStateNormal];
        NSString *strCheckboxTag = [NSString stringWithFormat:@"%d",ss.tag];
        
        
       if (![app.arrCheckBoxClikedId containsObject:strCheckboxTag])
       {
           [app.arrCheckBoxClikedId addObject:strCheckboxTag];
       }
        
        
        NSString *Quary = [NSString stringWithFormat:@"UPDATE tbl_shifa SET selected ='1' WHERE _id =%@",strCheckboxTag];
         [app.database executeUpdate:Quary];
        
        
    }else{
    

        self.isChecked =NO;
        [ss setImage:[UIImage imageNamed:@"cb_glossy_off.png"]forState:UIControlStateNormal];
        NSString *strCheckboxTag = [NSString stringWithFormat:@"%d",ss.tag];
        [app.arrCheckBoxClikedId removeObject:strCheckboxTag];
       
         NSString *Quary = [NSString stringWithFormat:@"UPDATE tbl_shifa SET selected ='0' WHERE _id =%@",strCheckboxTag];
        [app.database executeUpdate:Quary];
        
        
    }
 
    
}
-(void)getSelected_Ids
{
    NSString *QuaryString =[NSString stringWithFormat:@"SELECT _id FROM tbl_shifa where selected ='1'"];
    FMResultSet *results = [app.database executeQuery:QuaryString];
    while([results next]) {

        NSString *strTemp = [results stringForColumn:@"_id"];
      
    }
}




@end
