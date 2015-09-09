//
//  UIBorderLabel.m
//  Shifa
//
//  Created by My Mac on 5/6/14.
//  Copyright (c) 2014 Florence Technology. All rights reserved.
//

#import "UIBorderLabel.h"

@implementation UIBorderLabel
@synthesize topInset, leftInset, bottomInset, rightInset;


- (id)initWithFrame:(CGRect)frame
{
    self = [super initWithFrame:frame];
    if (self) {
        // Initialization code
    }
    return self;
}

- (void)drawTextInRect:(CGRect)rect
{
    UIEdgeInsets insets = {self.topInset, self.leftInset,
        self.bottomInset, self.rightInset};
    
    return [super drawTextInRect:UIEdgeInsetsInsetRect(rect, insets)];
}



@end
