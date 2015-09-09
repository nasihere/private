//
//  UIScrollView_CustomClass.m
//  Shifa
//
//  Created by My Mac on 5/12/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "UIScrollView_CustomClass.h"

@implementation UIScrollView_CustomClass

- (id)initWithFrame:(CGRect)frame
{
    self = [super initWithFrame:frame];
    if (self) {
        // Initialization code
    }
    return self;
}


- (void)touchesBegan:(NSSet *)touches withEvent:(UIEvent *)event {
    [super touchesBegan:touches withEvent:event];
   // NSLog(@"touchesBegan");
}

- (void)touchesEnded:(NSSet *)touches withEvent:(UIEvent *)event {
    [super touchesEnded:touches withEvent:event];
    //NSLog(@"touchesEnded");
}

- (void)touchesCancelled:(NSSet *)touches withEvent:(UIEvent *)event {
    [super touchesCancelled:touches withEvent:event];
   // NSLog(@"touchesCancelled");
}

@end
