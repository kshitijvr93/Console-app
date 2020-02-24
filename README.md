README.md

##
Application to access a bookstore as specified in the problem statement.

Default filetype handline: .txt
Default file read location: same as the .exe file generated after compilation

Sample .txt file data: 
book-id title author-name publication-name cost
1001 abc def gpub 10
1002 ghi jkl spub 12
1003 mno pqr kpub 100
1004 stu vwx spub 4

Supported commands:

#List all the books in the repo
Show Books 

#List all the books in the repo in ascending order
Show Books -sa

#List all the books in the repo in descending order
Show Books -sd

#List details of book with specific ID
BookId=<BookId>

#List details of books with specific Author including author info
Author=<AuthorName>

#List details of publication with specific publication
Publication=<PublicationName>

#Add info to specific author 
AddAuthorInfo

#Add info to specific publication 
AddPublicationInfo