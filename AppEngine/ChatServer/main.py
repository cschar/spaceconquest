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
        else:
            doPage = True

        if doPage : 
            self.response.out.write('''
<html> <head> <title> global chat client </title> </head>
<body>
''');

        chatlog_query = ChatLog.all().order('-date')
        chats = chatlog_query.fetch(5)
        
        for chat in chats:
            if doPage: 
                self.response.out.write('''<div style="margin: 2px; padding:10px; background-color: #CDCDCD;"> ''');
            self.response.out.write( '@ ' + str(chat.date) + ' -> ' + chat.author + ' wrote : ' + chat.content  + '\n')
            if doPage:
                self.response.out.write('</div>')

        if doPage:
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
