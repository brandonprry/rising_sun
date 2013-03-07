#/bin/sh

echo "Creating XML documentation..."
find . -name "AutoAssess*[.]dll" | while read p; do monodocer -assembly:"$p" -path:xml -pretty > /dev/null; done;

echo "Exporting to HTML..."
mdoc export-html -o en_html xml > /dev/null

find xml/ | grep [.]remove | xargs rm 

find . | grep [.]cs$ | xargs wc -l > linecount
