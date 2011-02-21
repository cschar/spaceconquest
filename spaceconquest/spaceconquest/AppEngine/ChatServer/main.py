#!/usr/bin/env python

from google.appengine.ext import webapp
from google.appengine.ext.webapp import util
from google.appengine.ext import db


class ChatLog(db.Model):
    author = db.StringProperty()
    content = db.StringProperty(multiline=True)
    date = db.DateTimeProperty(auto_now_add=True)


class MainHandler(webapp.RequestHandler):
    def get(self):

        
        if str(self.request.get('GetInfo')) == '1':
            doPage = False
            #self.response.out.write(' <html> <body> ')
        else:
            doPage = True

        if doPage : 
            self.response.out.write('''
<html> <head> <title> global chat client </title> </head>
<body>
''');

        chatlog_query = ChatLog.all().order('-date')
        chats = chatlog_query.fetch(10)
        
        for chat in chats:
            if doPage: 
                self.response.out.write('''<div style="margin: 2px; padding:10px; background-color: #CDCDCD;"> ''');
            
            if len(chat.author) > 10:
                name = chat.author[:10]
                
            else:
                name = chat.author

            #This makes sure all names are 10 characters in length
            while ( len(name) < 10):
                    name = name + " " 


            self.response.out.write( name + ' : ' + chat.content  + '\n')
            if doPage:
                self.response.out.write('</div>')
        
		
                self.response.out.write('</body></html>')

class SubmitHandler(webapp.RequestHandler):
    def get(self):        

        auth = str(self.request.get('name'))
        message = str(self.request.get('message'))
        
        if(auth != None and message != None):
            

            chatPost = ChatLog()
            chatPost.author = auth
            chatPost.content = message
            chatPost.put()

def main():
    application = webapp.WSGIApplication([('/', MainHandler),
                                          ('/Submit', SubmitHandler)],
                                         debug=True)
    util.run_wsgi_app(application)


if __name__ == '__main__':
    main()
