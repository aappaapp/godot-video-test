extends Button


func _on_pressed():
	FFmpeg.Initialize(ProjectSettings.globalize_path(Global.ffmpeg_path))
