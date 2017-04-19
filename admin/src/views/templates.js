import React, { Component } from 'react';
import { render } from 'react-dom';
import MonacoEditor from 'react-monaco-editor';

class templates extends Component {

    constructor(props){
        super(props);

        this.onChange = this.onChange.bind(this)

        this.state = {
            code: '@using System.Linq',
        }
    }

    editorDidMount(editor, monaco) {
        console.log('editorDidMount', editor);
        editor.focus();
    }

    onChange(newValue, e) {
        this.setState({code: newValue});
    }

    render() {
        const code = this.state.code;
        const options = {
            selectOnLineNumbers: true,
            roundedSelection: false,
            readOnly: false,
            theme: 'vs-dark',
            cursorStyle: 'line',
            automaticLayout: false,
        };
        const requireConfig = {
            url: 'https://cdnjs.cloudflare.com/ajax/libs/require.js/2.3.1/require.min.js',
            paths: {
                'vs': 'https://as.alipayobjects.com/g/cicada/monaco-editor-mirror/0.6.1/min/vs'
            }
        };
        return (
            <div>
                <MonacoEditor
                    width="800"
                    height="600"
                    language="razor"
                    options={options}
                    value={code}
                    requireConfig={requireConfig}
                    onChange={this.onChange}
                />
            </div>
        );
    }
}

export default templates;