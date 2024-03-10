# About

Basic intent is to show several methods to pass route parameters, normally, using data protection and revealing an encrypted string and using data protection without revealing route parameters in the browser address bar.

Besides the initial intent of this project for hiding or masking route parameters there are other things to learn about such as working with drop downs and EF Core.

## Use of temp data

Temp data is used so that a parameter can be passed without using data protection while two other examples use data protection.

## Data Operations

Are done in pages rather than uses services so that the reader can see everything.

For real applications consider using dependency injection with services and/or controllers and views. The important this is to focus on routing which is detached from backend code other than encrypting and decrypting route data.