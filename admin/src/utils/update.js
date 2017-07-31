// instantiate object update function
export default function update (targetObject, obj) {
  Object.keys(obj).forEach(function (key) {

    // delete property if set to undefined or null
    if ( undefined === obj[key] || null === obj[key] ) {
      delete targetObject[key]
    }

    // property value is object, so recurse
    else if ( 
        'object' === typeof obj[key] 
        && !Array.isArray(obj[key]) 
    ) {

      // target property not object, overwrite with empty object
      if ( 
        !('object' === typeof targetObject[key] 
        && !Array.isArray(targetObject[key])) 
      ) {
        targetObject[key] = {}
      }

      // recurse
      update(targetObject[key], obj[key])
    }

    // set target property to update property
    else {
      targetObject[key] = obj[key]
    }
  })
}