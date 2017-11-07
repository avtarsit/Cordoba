
app.filter('unique', function () {
    return function (collection, keyname) {
        var output = [],
            keys = [];

        angular.forEach(collection, function (item) {
            var key = item[keyname];
            if (keys.indexOf(key) === -1) {
                keys.push(key);
                output.push(item);
            }
        });
        return output;
    };
});


app.filter('sum', function () {
    return function (ListRecords,FieldForSum) {
        var total = 0;
        for (i = 0; i < ListRecords.length; i++) {
            total = total + ListRecords[i][FieldForSum];
        };
        return total;
    };
});

app.filter('htmlToPlaintext', function () {
    return function (text) {
        return angular.element(text).text();
    }
});

app.filter('trusted', ['$sce', function ($sce) {
    return function (url) {
        return $sce.trustAsResourceUrl(url);
    };
}]);