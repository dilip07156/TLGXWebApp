<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierScheduleTask.aspx.cs" Inherits="TLGX_Consumer.suppliers.SupplierScheduleTask" %>

<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.3/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.3.1/css/all.css">
    <link rel="stylesheet" href="style.css">
    <title></title>
</head>

<body>
    <br>
    <div class="container">
        <hr>
        <h2>Main Navigation</h2>
        <hr>
        <h1>Pending Static Data File Handling</h1>
        <br>
        <div class="card">
            <div class="card-header">
                Search Tasks
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <div class="form-group">
                            <label for="exampleFormControlSelect1">Supplier</label>
                            <select class="form-control" id="exampleFormControlSelect1">
                                <option>-Select-</option>
                                <option>Active</option>
                                <option>Suppliers</option>
                                <option>File</option>
                                <option>AI+File</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="exampleFormControlSelect1">Entity</label>
                            <select class="form-control" id="exampleFormControlSelect1">
                                <option>-Select-</option>
                                <option>List</option>
                                <option>From</option>
                                <option>Entity</option>
                                <option>Masters</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="exampleFormControlSelect1">Status</label>
                            <select class="form-control" id="exampleFormControlSelect1">
                                <option>-Select-</option>
                                <option>Active</option>
                                <option>Suppliers</option>
                                <option>File</option>
                                <option>AI+File</option>
                            </select>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label for="exampleFormControlInput1">From </label>
                            <input type="date" class="form-control" id="exampleFormControlInput1" placeholder="name@example.com">
                        </div>
                        <div class="form-group">
                            <label for="exampleFormControlInput1">To </label>
                            <input type="date" class="form-control" id="exampleFormControlInput1" placeholder="name@example.com">
                        </div>
                        <br>
                        <button type="button" class="btn btn-primary">Search</button>
                        <button type="button" class="btn btn-primary">Reset</button>
                    </div>
                </div>
            </div>
        </div>
        <br>
        <div class="row">
            <div class="col-8">
                <h3>Search Results</h3>
            </div>
            <div class="col-4">
                <div class="form-group">
                    <label for="exampleFormControlSelect1">page Size</label>
                    <select class="form-control" id="exampleFormControlSelect1">
                        <option>5</option>
                        <option>10</option>
                        <option>15</option>
                        <option>20</option>
                        <option>25</option>
                    </select>
                </div>
            </div>
        </div>
        <table class="table table-striped">
            <thead>
                <th>Supplier Name</th>
                <th>Entity</th>
                <th>Status</th>
                <th>Scheduled Date</th>
                <th>Pending For (Days)</th>
                <th colspan="3"></th>
            </thead>
            <tbody>
                <tr>
                    <td>AOT</td>
                    <td>Country</td>
                    <td>Pending</td>
                    <td>01-NOV-2018</td>
                    <td>3</td>
                    <td>
                        <button type="button" class="btn btn-secondary">Download Instructions</button></td>
                    <td>
                        <button type="button" class="btn btn-primary">Upload File</button></td>
                    <td>
                        <button type="button" class="btn btn-success">Task Completed</button></td>
                </tr>
                <tr>
                    <td>AOT</td>
                    <td>City</td>
                    <td>Pending</td>
                    <td>01-NOV-2018</td>
                    <td>3</td>
                    <td>
                        <button type="button" class="btn btn-secondary">Download Instructions</button></td>
                    <td>
                        <button type="button" class="btn btn-primary">Upload File</button></td>
                    <td>
                        <button type="button" class="btn btn-success">Task Completed</button></td>
                </tr>
                <tr>
                    <td>AOT</td>
                    <td>Hotel</td>
                    <td>Pending</td>
                    <td>01-NOV-2018</td>
                    <td>3</td>
                    <td>
                        <button type="button" class="btn btn-secondary">Download Instructions</button></td>
                    <td>
                        <button type="button" class="btn btn-primary">Upload File</button></td>
                    <td>
                        <button type="button" class="btn btn-success">Task Completed</button></td>
                </tr>
                <tr>
                    <td>AOT</td>
                    <td>RoomType</td>
                    <td>Pending</td>
                    <td>01-NOV-2018</td>
                    <td>3</td>
                    <td>
                        <button type="button" class="btn btn-secondary">Download Instructions</button></td>
                    <td>
                        <button type="button" class="btn btn-primary">Upload File</button></td>
                    <td>
                        <button type="button" class="btn btn-success">Task Completed</button></td>
                </tr>

            </tbody>

        </table>



        <nav aria-label="Page navigation example">
            <ul class="pagination">
                <li class="page-item"><a class="page-link" href="#">Previous</a></li>
                <li class="page-item"><a class="page-link" href="#">1</a></li>
                <li class="page-item"><a class="page-link" href="#">2</a></li>
                <li class="page-item"><a class="page-link" href="#">3</a></li>
                <li class="page-item"><a class="page-link" href="#">Next</a></li>
            </ul>
        </nav>

        <hr>
        <div class="modal-dialog-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Download Instructions</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>
                        To Download the static data for %SUPPLIERNAME% please follow the below Instructions
                        <form>
                            <div class="form-group">
                                <label for="exampleFormControlInput1">URL</label>
                                <input type="email" class="form-control" id="exampleFormControlInput1" placeholder="name@example.com">
                            </div>

                            <div class="form-group row">
                                <label for="staticEmail" class="col-sm-2 col-form-label">Username</label>
                                <div class="col-sm-10">
                                    <input type="email" class="form-control" id="exampleFormControlInput1">
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="inputPassword" class="col-sm-2 col-form-label">Password</label>
                                <div class="col-sm-10">
                                    <input type="password" class="form-control" id="inputPassword" placeholder="Password">
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="exampleFormControlTextarea1">Description</label>
                                <textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
                            </div>
                        </form>

                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.13.0/umd/popper.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.1.3/js/bootstrap.min.js"></script>
</body>

</html>
