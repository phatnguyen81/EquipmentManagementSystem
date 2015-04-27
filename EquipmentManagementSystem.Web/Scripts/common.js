function parseDate(str) {
    var dmy = str.toString().split('/');
    return new Date(dmy[2], dmy[1] - 1, dmy[0]);
}

function daydiff(first, second) {
    return (second - first) / (1000 * 60 * 60 * 24);
}