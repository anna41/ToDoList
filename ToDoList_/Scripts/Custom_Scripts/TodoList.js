//for checkBox
$(document).ready(function () {

    $('.ActiveCheck').change(function () {

        var self = $(this);
        var id = self.attr('id');
        var value = self.prop('checked');

        $.ajax({
            url: '/Plans/AjaxEdit',
            data: {
                id: id,
                value: value
            },
            type: 'POST',
            success: function (result) {
                $('.container.body-content').html(result);
                location.reload();
            }
        });
    });
});


//delete items
    onDelete = function (id) {      
        $.ajax({           
            url: '/Plans/Delete',
            data: {
                id: id,
            },
            type: 'POST',
            success: function (result) {
                $('.container.body-content').html(result);
                location.reload();
            }
        });
    };


//returns items that you need
    allDone = function () {

        $.ajax({
            url: '/Plans/AllDone',
            type: 'GET',
            success: function (result) {
                $('.container.body-content').html(result);
            }
        });
    };

    allPlans = function () {

        $.ajax({
            url: '/Plans/AllPlans',
            type: 'GET',
            success: function (result) {
                $('.container.body-content').html(result);
                location.reload();
            }
        });
    };

  
    allNotDone = function () {

        $.ajax({
            url: '/Plans/AllNotDone',
            type: 'GET',
            success: function (result) {
                $('.container.body-content').html(result);
               
            }
        });
    };


//to edit text 

    $(document).ready(function () {
        $('.editText').click(function () {
            var div = $(this);
            var taskEditor = div.find('input');           
            taskEditor.val(div.text().trim());//set text box value from div current text                   
            taskEditor.show();
            div.find('span').hide();
            taskEditor.focus();//put text box on focus           
        });
        $('.editText input').focusout(function () {
            var m = $(this).val();
            var parentDiv = $(this).parent();
            parentDiv.find('input').remove().end().html(m);
            $.ajax({
                url: '/Plans/EditDescription',
                data: {
                    id: parentDiv[0].id,
                    text: m,
                },
                type: 'POST',
                success: function (result) {
                    $('.container.body-content').html(result);
                    location.reload();
                }
            });
        });
   
    });

//don't work yet
    saveAfterLogin = function () {

            $.ajax({
                url: '/Plans/SaveAllPlansAfterLogin',             
                type: 'GET',
                success: function (result) {
                    $('.container.body-content').html(result);
                }
            });
        };


      