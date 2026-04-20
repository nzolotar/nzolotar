const arr = [1,2,3,4,5];

const reduced = arr.reduce(function (accumulator, current){
    accumulator = accumulator + current;
    return accumulator;
  });

  console.log('max reduce function', reduced);

  const max = arr.reduce(function (accumulator, current){
    if(current > accumulator){
      accumulator = current;
    }
    return accumulator;
  } , 0);
  console.log('max reduce function', max);

  const map = arr.map(function (current){
    return current * 2;
  } );  
  
  console.log('map', map);
  
  const filter = arr.filter(function (current){
    return current > 2;
  } );  
  console.log('filter', filter);  
  
  const find = arr.find(function (current){ //find returns the first element that satisfies the condition
    return current > 2;
  } );  
  console.log('find', find);  
  
  const findIndex = arr.findIndex(function (current){ //findIndex returns the index of the first element that satisfies the condition   
    return current > 2;
  } );  
  console.log('findIndex', findIndex);  
  
  const includes = arr.includes(3);  console.log('includes', includes);  
  
  const some = arr.some(function (current){ //some returns true if at least one element satisfies the condition    return current > 2;   
  } );  console.log('some', some);  
  
  const every = arr.every(function (current){ //every returns true if all elements satisfy the condition    return current > 2; 
  } );  console.log('every', every);  
  
  const sort = arr.sort(function (a, b){ //sort returns a new array sorted according to the order specified by the compare function    return a - b;
  } );  console.log('sort', sort);
