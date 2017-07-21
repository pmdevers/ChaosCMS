import traverson from 'traverson';
import JsonHalAdapter from 'traverson-hal';

traverson.registerMediaType(JsonHalAdapter.mediaType, JsonHalAdapter);

export function fetch(path, func){
    return traverson
        .from('http://localhost:17706/api')
        .jsonHal()
        .follow(path)
        .getResource(function(error, document, traversal) {
        if (error) {
            console.error('No luck :-)', error)
        } 
        func(document, traversal);
    });
}

export function fetchFrom(location, path, func){
    return traverson
        .from('http://localhost:17706' + location)
        .jsonHal()
        .follow(path)
        .getResource(function(error, document, traversal) {
        if (error) {
            console.error('No luck :-)', error)
        } 
        func(document, traversal);
    });
}
