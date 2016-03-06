var ComentarioForm = React.createClass({
        prueba : function(e) {
            console.log("Cancelado");
            e.preventDefault();
            return;
        },
        render: function() {
            return (
                <form className="form-inline" onSubmit={this.prueba}>
                    <div className="form-group">
                        <input type="text" className="form-control" placeholder="Tú Alias" ref="author"/>
                    </div>
                    <div className="form-group">
                        <input type="text" className="form-control" placeholder="Comenta algo..." ref="text"/>
                    </div>  
                    <input type="submit" value="Enviar" className="btn btn-primary"/>
                </form>
            );
        }
    }
);

ReactDOM.render(<ComentarioForm />,document.getElementById("content") );