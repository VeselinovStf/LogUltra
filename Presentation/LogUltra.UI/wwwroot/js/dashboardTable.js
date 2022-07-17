

$(document).ready(function () {
    var table = $('#logsTable').DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Dashboard/List",
            "type": "POST",
            "datatype": "json",
            "data": function (data) {
                // Read values
                var level = $('#searchByLevel').val();
                var source = $('#searchBySource').val();
                var exception = $('#searchByException').val();

                // Append to data 
                data.level = level;
                data.source = source;
                data.exception = exception;
            },
            "dataSrc": function (json) {
                //Make your callback here.

                let levels = $('#searchByLevel option').map(function () {
                    return this.value;
                }).get();

                $.each(json.data, function (i, el) {
                    if (!levels.includes(el.level)) {
                        $('#searchByLevel').append(`<option value='${el.level}'>${el.level}</option>`)
                        levels.push(el.level);
                    }
                })

                let sources = $('#searchBySource option').map(function () {
                    return this.value;
                }).get();

                $.each(json.data, function (i, el) {
                    if (!sources.includes(el.source)) {
                        $('#searchBySource').append(`<option value='${el.source}'>${el.source}</option>`)
                        sources.push(el.source);
                    }
                })

                let exceptions = $('#searchByException option').map(function () {
                    return this.value;
                }).get();

                $.each(json.data, function (i, el) {
                    let getValue = el.isException.toString().toLowerCase();
                    if (!exceptions.includes(getValue)) {
                        $('#searchByException').append(`<option value='${getValue}'>${getValue}</option>`)
                        exceptions.push(getValue);
                    }
                })

                return json.data;
            }
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "createdAt", "name": "CreatedAt", "autoWidth": true },
            { "data": "level", "name": "Level", "autoWidth": true },
            { "data": "source", "name": "Source", "autoWidth": true },
            { "data": "description", "name": "Description", "autoWidth": true },
            { "data": "isException", "name": "IsException", "autoWidth": true },
            { "data": "exception", "name": "Exception", "autoWidth": true }          
        ]


    });

    $('#searchByLevel').change(function () {
        table.draw();
    });

    $('#searchBySource').change(function () {
        table.draw();
    });

    $('#searchByException').change(function () {
        table.draw();
    });


});

