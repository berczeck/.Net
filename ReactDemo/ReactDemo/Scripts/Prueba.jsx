var Comment = React.createClass({
		render : function(){
			var converter = new Showdown.converter();
			var rawMarkup = converter.makeHtml(this.props.children.toString());
			var titulo = 
				this.props.author.concat(!this.props.fechaHora ? " - Procesando" : " - ".concat(this.props.fechaHora));
			return (
				<a href="#" className="list-group-item">
					<h4 className="list-group-item-heading">{titulo}</h4>
					<span className="list-group-item-text" dangerouslySetInnerHTML={{__html: rawMarkup}} />
				</a>	
			);
		}
	}
);

var CommentList = React.createClass({
		render : function (){
			var contador=0;
			var commentNodes = this.props.data.map(function(comment){
					contador++;					
					return <Comment key={contador} author={comment.Author} fechaHora={comment.FechaHora}>{comment.Text}</Comment>
				}
			);
			return (			
				<div className="list-group">	
					{commentNodes}					
				</div>
			);
		}
	}
);

var CommentForm = React.createClass({
		handleSubmit: function(e) {
			e.preventDefault();
			var author = this.refs.author.getDOMNode().value.trim();
			var text = this.refs.text.getDOMNode().value.trim();
			if (!text || !author) {
				return;
			}
			// TODO: send request to the server
			this.refs.author.getDOMNode().value = '';
			this.refs.text.getDOMNode().value = '';
			return;
		},
		render : function(){
			return (
				<form className="form-inline" onSubmit={this.handleSubmit}>
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

var CommentBox = React.createClass({
		handleCommentSubmit : function(comment){
			var comments = this.state.data;
			comments.unshift(comment);
			this.setState({ data: comments });

			var data = new FormData();
			data.append('Author',comment.Author);
			data.append('Text',comment.Text);

			var xhr = new XMLHttpRequest();
			xhr.open('post', this.props.submitUrl, true);
			xhr.send(data);
		},
		getInitialState : function(){
			return {data:[]};
		},
		cargarComentarios : function(comments){
			this.setState({data: comments});
		},
		componentDidMount : function(){
			var commentHub = $.connection.commentHub;
			$.connection.hub.start();

			commentHub.client.loadComments = this.cargarComentarios
		},
		render: function(){
			return (
				<div className="commentBox">
					<h1>Comentarios</h1>
					<CommentForm onCommentSubmit={this.handleCommentSubmit}/>
					<CommentList data={this.state.data}/>
				</div>
			);
		}
	}
);

ReactDOM.render(<CommentBox />,document.getElementById("content") );