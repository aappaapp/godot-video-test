extends FileDialog


func _on_open_button_pressed() -> void:
	visible = true


func _on_dir_selected(dir: String):
	Global.ffmpeg_path = dir
