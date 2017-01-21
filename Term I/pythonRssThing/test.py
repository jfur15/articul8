import feedparser
d = feedparser.parse('http://rss.cnn.com/rss/cnn_topstories.rss')

d['feed']['title']
i = 50
while (i > 0):
	print d.entries[i].title
	print d.entries[i].summary.split("<")[0] + '\n'
	print '---------------------------------   \n'
	i = i-1

